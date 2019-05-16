using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap
{

    public class ConzapMenuItem
    {
        public ConzapMenuItem(string header, Action callBack)
        {
            if (callBack != null)
            {
                Callback = callBack;
            }
            if (!string.IsNullOrEmpty(header))
            {
                Header = header;
            }
        }

        public ConzapMenuItem()
        {

        }
        public string Header { get; set; }
        public Action Callback { get; set; }
    }
}
