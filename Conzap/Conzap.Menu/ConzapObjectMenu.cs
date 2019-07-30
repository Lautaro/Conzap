using Conzap.ObjectPrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap.Menu
{
    public class ConzapObjectMenu<T> : ConzapObjectPrinter<T>
    {
        public Func<T, string> TitleFactory { get; set; }
        public string Header { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public bool ClearScreen { get; set; }

        public ConzapObjectMenu(IEnumerable<T> objects, Func<T, string> titleFactory, string header = "", string message = "", string errorMessage = "Not an option. Try again..", bool clearSreen = false) : base(objects)
        {
            Header = header;
            Message = message;
            ErrorMessage = errorMessage;
            ClearScreen = clearSreen;
            TitleFactory = titleFactory;
        }

        public override void Run()
        {
            var item = ConzapTools.AskForListChoice<T>(Objects, TitleFactory, Header, Message, ErrorMessage, ClearScreen);
            Conzap.ConzapToolHelpers.ClearScreen(ClearScreen);
            PrintThese.PrintObject<T>(item);

        }

        public override ConzapObjectPrinter<T> AddAndRun(string label, Func<T, string> printThis)
        {
            PrintThese.Add(new ObjectPrinterField<T>(label, printThis));
            this.Run();
            return this;
        }
    }
}
