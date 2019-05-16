using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conzap
{
  public class ConzapMenu
    {
        public List<ConzapMenuItem> MenuItems { get; set; }
        public string Header { get; set; }

        public ConzapMenu(string header = "Options", List<ConzapMenuItem> menuItems = null)
        {
            if (menuItems != null && menuItems.Count >0)
            {
                MenuItems = menuItems;
            }
            else
            {
                MenuItems = new List<ConzapMenuItem>();
            }
            if (!string.IsNullOrEmpty(header))
            {
                Header = header;
            }
            
        }
    }
}
