using System;

namespace Conzap.Tools
{
    public class ConzapPrintThis<T>
    {
        public string Label { get; set; }
        public Func<T, string> PrintThis { get; set; }

        public ConzapPrintThis(string label, Func<T, string> printThis)
        {
            Label = label;
            PrintThis = printThis;
        }
    }
}
