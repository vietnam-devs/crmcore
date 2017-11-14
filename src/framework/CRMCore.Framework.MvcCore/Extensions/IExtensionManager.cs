using System;
using System.Collections.Generic;

namespace CRMCore.Framework.MvcCore.Extensions
{
    public interface IExtensionManager
    {
        ExtensionInfo GetExtension(string extensionId);
        IEnumerable<ExtensionEntry> GetExtensions();
    }
}