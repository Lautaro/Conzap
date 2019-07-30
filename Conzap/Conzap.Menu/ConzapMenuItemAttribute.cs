using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap.Menu
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ConzapMenuItemAttribute : System.Attribute
    {
        public int Index { get; set; }
        public string Header { get; set; }

        public ConzapMenuItemAttribute()
        {

        }

        public ConzapMenuItemAttribute(string header, int index)
        {
            Header = header;
            Index = index;

        }
    }
}
