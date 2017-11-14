using System;
using Microsoft.Extensions.FileProviders;

namespace CRMCore.Framework.MvcCore.Extensions
{
    public class ExtensionInfo
    {
        private readonly string _id;
        private readonly string _subPath;
        private readonly IFileInfo _fileInfo;
       
        public ExtensionInfo(
            string id,
            IFileInfo fileInfo,
            string subPath)
        {

            _id = id;
            _fileInfo = fileInfo;
            _subPath = subPath;
        }

        public string Id => _id;
        public string SubPath => _subPath;
        public IFileInfo ExtensionFileInfo => _fileInfo;
        public bool Exists => _fileInfo.Exists;
    }
}
