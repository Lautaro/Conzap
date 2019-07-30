using System;

namespace Conzap.ObjectPrinting
{
    public class ObjectPrinterField<T>
    {
        public string Label { get; set; }
        public Func<T, string> PrintThis { get; set; }

        public ObjectPrinterField(string label, Func<T, string> printThis)
        {
            Label = label;
            PrintThis = printThis;
        }
    }
}
