using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Conzap.ObjectPrinting
{
    /// <summary>
    /// DONT USE THIS! Its deprecated. Use ObjectPrinter. Over and out.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConzapObjectPrinter<T>
    {
        public List<ObjectPrinterField<T>> PrintThese { get; set; } = new List<ObjectPrinterField<T>>();
        public IEnumerable<T> Objects { get; set; }

        public ConzapObjectPrinter() { }
        public ConzapObjectPrinter(IEnumerable<T> objects)
        {
            Objects = objects;
        }
        
        public ConzapObjectPrinter<T> Add(string label, Func<T, string> printThis)
        {
            PrintThese.Add(new ObjectPrinterField<T>(label, printThis));
            return this;
        }

        public virtual ConzapObjectPrinter<T> AddAndRun(string label, Func<T, string> printThis)
        {
            PrintThese.Add(new ObjectPrinterField<T>(label, printThis));
            Run();
            return this;
        }

        public virtual void Run()
        {
            if (Objects == null)
            {
                throw new Exception("Objects cant be null. Specify a collection of T that should be printed.");

            }
         PrintStuff.PrintObjectList(PrintThese ,Objects);
        }
    }


}
