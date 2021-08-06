using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

namespace Dashboard
{
    class Menus : ObservableCollection<MenuItemType>
    {
        public Menus()
        {

        }

        public void MenusAdd(string content)
        {

            Add(new MenuItemType(content));

        }
    }
}
