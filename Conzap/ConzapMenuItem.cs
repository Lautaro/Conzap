using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap
{

    public class ConzapMenuItem
    {
        public ConzapMenuItem(string title, Action callBack)
        {
            Callback = callBack;
            Title = title;
        }

        /// <summary>
        /// MenuItems title will be method name
        /// </summary>
        public ConzapMenuItem(Action callBack)
        {
            Callback = callBack;
            Title = Callback.Method.Name;
        }

        public ConzapMenuItem()
        {

        }

        public string Title { get; set; }
        public Action Callback { get; set; }
    }
}
