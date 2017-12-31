using System;
using System.Collections.Generic;

namespace CRMCore.Module.MvcCore.Extensions
{
    public interface IExtensionManager
    {
        ExtensionInfo GetExtension(string extensionId);
        IEnumerable<ExtensionEntry> GetExtensions();
    }
}