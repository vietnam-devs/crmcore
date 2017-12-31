using System.Collections.Generic;

namespace CRMCore.Module.MvcCore.Options
{
    public class ExtensionExpanderOptions
    {
        public IList<ExtensionExpanderOption> Options { get; }
            = new List<ExtensionExpanderOption>();
    }

    public class ExtensionExpanderOption
    {
        public string SearchPath { get; set; }
    }
}
