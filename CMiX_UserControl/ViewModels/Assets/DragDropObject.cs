using CMiX.Studio.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Studio.ViewModels
{
    public class DragDropObject
    {
        public DragDropObject()
        {
            SourceCollection = new ObservableCollection<IAssets>();
        }

        public IAssets DragObject { get; set; }
        public ObservableCollection<IAssets> SourceCollection { get; set; }
    }
}