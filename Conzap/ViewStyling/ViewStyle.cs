using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap.ViewStyling
{
    public class ViewStyle
    {
        public HeadingStyle HeadingStyle { get; set; } = new HeadingStyle();
        public bool ClearScreen { get; set; } = true;
        public string WaitMessage { get; set; } = null;
        public ConsoleKey? WaitKey { get; set; } = null;
        public ConsoleListStyle ListStyle { get; set; } = ConsoleListStyle.Asterisk;
        public int ListSkip { get; set; } = 0;
        public string ListDelimiter { get; set; } = null;
        public string QuitItemTitle { get; set; } = "Quit";
        public string ErrorMessage { get; set; } = null;

    }
}
