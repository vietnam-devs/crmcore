using System;
using System.Collections.Generic;
using System.Reflection;

namespace CRMCore.Mvc.Core.Extensions
{
    public class ExtensionEntry
    {
        public ExtensionInfo ExtensionInfo { get; set; }
        public Assembly Assembly { get; set; }
        public IEnumerable<Type> ExportedTypes { get; set; }
        public bool IsError { get; set; }
    }
}
