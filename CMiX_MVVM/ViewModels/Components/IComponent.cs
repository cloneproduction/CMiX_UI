using CMiX.MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public interface IComponent
    {
        ObservableCollection<Component> Components { get; set; }
        int ID { get; set; }
        string Name { get; set; }

        bool IsEditing { get; set; }

        bool IsRenaming { get; set; }
    }
}
