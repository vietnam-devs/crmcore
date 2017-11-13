using System;
using System.Collections.Generic;

namespace CRMCore.Mvc.Core.Options
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
