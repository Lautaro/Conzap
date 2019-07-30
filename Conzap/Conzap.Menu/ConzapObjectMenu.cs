using Conzap.ObjectPrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap.Menu
{
    public class ConzapObjectMenu<T>
    {
        public Func<T, string> TitleFactory { get; set; }
        public string Header { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public bool ClearScreen { get; set; }
        public IEnumerable<T> Objects { get; set; }
        internal List<ObjectPrinterField<T>> PrintThese { get; set; } = new List<ObjectPrinterField<T>>();

        public ConzapObjectMenu(IEnumerable<T> objects, Func<T, string> titleFactory, string header = "", string message = "", 
            string errorMessage = "Not an option. Try again..", bool clearSreen = false) 
        {
            Header = header;
            Message = message;
            ErrorMessage = errorMessage;
            ClearScreen = clearSreen;
            TitleFactory = titleFactory;
            Objects = objects;
        }

        public void Print()
        {
            var item = ConzapTools.AskForListChoice<T>(Objects, TitleFactory, Header, Message, ErrorMessage, ClearScreen);
            Conzap.ConzapToolHelpers.ClearScreen(ClearScreen);
            ConzapTools.PrintObject<T>(item);

        }

        public ConzapObjectMenu<T> AddAndPrint(string label, Func<T, string> printThis)
        {
            PrintThese.Add(new ObjectPrinterField<T>(label, printThis));
            this.Print();
            return this;
        }
    }
}
