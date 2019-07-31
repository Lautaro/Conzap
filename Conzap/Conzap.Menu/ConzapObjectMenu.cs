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

        public ConzapObjectMenu(IEnumerable<T> objects, Func<T, string> titleFactory, string heading = "", string message = "", 
            string errorMessage = "Not an option. Try again..", bool clearSreen = false) 
        {
            Style.HeadingStyle.Text = heading;
            Style.ErrorMessage = errorMessage;
            Style.ClearScreen = clearSreen;
            TitleFactory = titleFactory;
            Objects = objects;
        }

        public void Print()
        {
            var item = ConzapTools.ChooseFromList<T>(Objects, TitleFactory, Style);
            Conzap.ConzapToolHelpers.ClearScreen(Style.ClearScreen);
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
