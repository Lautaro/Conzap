using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap.ViewStyling
{
    public class ViewStyle
    {
        public static ViewStyle New(string HeadingText = "", bool clearScreen = false)
        {
            var style = new ViewStyle()
            {
                ClearScreen = clearScreen
            };

            style.HeadingStyle.Text = HeadingText;

            return style;
        }
        public HeadingStyle HeadingStyle { get; set; } = new HeadingStyle();
        public bool ClearScreen { get; set; } = true;
        public string WaitMessage { get; set; } = null;
        public ConsoleKey? WaitKey { get; set; } = null;
        public ConsoleListStyle ListStyle { get; set; } = ConsoleListStyle.Asterisk;
        public int ListSkip { get; set; } = 0;
        public string ListDelimiter { get; set; } = null;
        public string QuitItemTitle { get; set; } = "Quit";
        public string ErrorMessage { get; set; } = null;

        public ViewStyle ClearScreenTrue()
        {
            ClearScreen = true;
            return this;
        }

        public ViewStyle ClearScreenFalse()
        {
            ClearScreen = false;
            return this;
        }

        public ViewStyle SetClearScreen(bool clearScreen)
        {
            ClearScreen = clearScreen;
            return this;
        }

        public ViewStyle SetHeadingText(string text)
        {
            HeadingStyle.SetText(text);
            return this;
        }
    }
}
