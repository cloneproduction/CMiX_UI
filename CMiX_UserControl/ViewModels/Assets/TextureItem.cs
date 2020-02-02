using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
{
    public class TextureItem : Item
    {
        public TextureItem(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
