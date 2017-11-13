using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CRMCore.Mvc.Core.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CRMCore.Mvc.Core.Extensions
{
    public class ExtensionManager : IExtensionManager
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ExtensionExpanderOptions _extensionExpanderOptions;
        private readonly IFileProvider _fileProvider;
        private readonly ILogger _logger;

        private bool _isInitialized = false;
        private static object InitializationSyncLock = new object();

        private IDictionary<string, ExtensionEntry> _extensions;

        public ExtensionManager(IHostingEnvironment hostingEnvironment,
                                IOptions<ExtensionExpanderOptions> extensionExpanderOptionsAccessor,
                                ILogger<ExtensionManager> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _extensionExpanderOptions = extensionExpanderOptionsAccessor.Value;
            _fileProvider = hostingEnvironment.ContentRootFileProvider;
            _logger = logger;
        }

        public ExtensionInfo GetExtension(string extensionId)
        {
            EnsureInitialized();

            ExtensionEntry extension;
            if (!String.IsNullOrEmpty(extensionId) && _extensions.TryGetValue(extensionId, out extension))
            {
                return extension.ExtensionInfo;
            }

            return null;
        }

        public IEnumerable<ExtensionEntry> GetExtensions()
        {
            EnsureInitialized();

            return _extensions.Values;
        }

        private void EnsureInitialized()
        {
            if (_isInitialized)
            {
                return;
            }

            lock (InitializationSyncLock)
            {
                if (_isInitialized)
                {
                    return;
                }

                var extensions = HarvestExtensions();

                var loadedExtensions = new ConcurrentDictionary<string, ExtensionEntry>();
                // Load all extensions in parallel
                Parallel.ForEach(extensions, (extension) =>
                {
                    if (!extension.Exists)
                    {
                        return;
                    }

                    var entry = GetExtensionEntry(extension);

                    if (entry.IsError && _logger.IsEnabled(LogLevel.Warning))
                    {
                        _logger.LogWarning("No loader found for extension \"{0}\". This might denote a dependency is missing or the extension doesn't have an assembly.", extension.Id);
                    }

                    loadedExtensions.TryAdd(extension.Id, entry);
                });

                _extensions = loadedExtensions;

                _isInitialized = true;
            }
        }

        private ISet<ExtensionInfo> HarvestExtensions()
        {
            var searchOptions = _extensionExpanderOptions.Options;
            var extensionSet = new HashSet<ExtensionInfo>();
            if (searchOptions.Count == 0)
            {
                return extensionSet;
            }

            foreach (var searchOption in searchOptions)
            {
                foreach (var subFolder in _hostingEnvironment
                   .ContentRootFileProvider
                     .GetDirectoryContents("packages")
                     .Where(x => x.IsDirectory))
                {
                    var manifestsubPath = searchOption.SearchPath + '/' + subFolder.Name;
                    var extensionInfo = GetExtensionInfo(manifestsubPath);

                    extensionSet.Add(extensionInfo);
                }
            }

            return extensionSet;
        }

        private ExtensionInfo GetExtensionInfo(string subPath)
        {
            var path = System.IO.Path.GetDirectoryName(subPath);
            var name = System.IO.Path.GetFileName(subPath);

            var extension = _fileProvider
                .GetDirectoryContents(path)
                .First(content => content.Name == name);

            return new ExtensionInfo(extension.Name, extension, subPath);
        }

        private ExtensionEntry GetExtensionEntry(ExtensionInfo extensionInfo)
        {
            try
            {
                var assembly = Assembly.Load(new AssemblyName(extensionInfo.Id));

                if (assembly == null)
                {
                    return null;
                }

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Loaded referenced ambient extension \"{0}\": assembly name=\"{1}\"", extensionInfo.Id, assembly.FullName);
                }

                return new ExtensionEntry
                {
                    ExtensionInfo = extensionInfo,
                    Assembly = assembly,
                    ExportedTypes = assembly.ExportedTypes
                };
            }
            catch
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("An extension found but was not loaded: \"{0}\". It might denote an extension which was not referenced by the running application project.", extensionInfo.Id);
                }

                return null;
            }
        }
    }
}
