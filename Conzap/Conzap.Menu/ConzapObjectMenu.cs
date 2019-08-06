using Conzap.ObjectPrinting;
using Conzap.ViewStyling;
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
        public ViewStyle Style { get; set; } = new ViewStyle();
        public IEnumerable<T> Objects { get; set; }
        internal List<ObjectPrinterField<T>> PrintThese { get; set; } = new List<ObjectPrinterField<T>>();
        public string Heading { get; set; }

        public ConzapObjectMenu(IEnumerable<T> objects, Func<T, string> titleFactory, string heading = "", string message = "", 
            string errorMessage = "Not an option. Try again..") 
        {
            Heading = heading;
            Style.ErrorMessage = errorMessage;
            TitleFactory = titleFactory;
            Objects = objects;
        }

        public void Print(bool clearScreen = true)
        {
            var item = ConzapTools.ChooseFromList<T>(Objects, TitleFactory, Style);
            Conzap.ConzapToolHelpers.ClearScreen(clearScreen);
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
