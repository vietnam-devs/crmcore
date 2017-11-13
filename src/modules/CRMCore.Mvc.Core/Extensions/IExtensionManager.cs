using System;
using System.Collections.Generic;

namespace CRMCore.Mvc.Core.Extensions
{
    public interface IExtensionManager
    {
        ExtensionInfo GetExtension(string extensionId);
        IEnumerable<ExtensionEntry> GetExtensions();
    }
}