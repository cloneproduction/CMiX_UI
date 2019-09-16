using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMiX.MVVM.ViewModels;

namespace CMiX.ViewModels
{
    public class DirectoryItem : Item
    {
        public DirectoryItem()
        {
            Items = new List<Item>();
        }

        private List<Item> _items;
        public List<Item> Items
        {
            get { return _items; }
            set { _items = value; }
        }
    }
}
