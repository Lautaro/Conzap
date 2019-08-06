using Conzap.ViewStyling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap
{
    public class GlobalViewStyle
    {
        public static ViewStyle Style { get ; set; } = new ViewStyle();
        public static void Set(ViewStyle style)
        {
            Style = style;
        }
    }
}
