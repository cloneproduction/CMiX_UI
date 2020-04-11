using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models
{
    public interface IComponentModel : IModel
    {
        int ID { get; set; }
        string Name { get; set; }
        string MessageAddress { get; set; }
        bool IsVisible { get; set; }
        //BeatModel BeatModel { get; set; }
        //MessageServiceModel MessageServiceModel { get; set; }

        ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}
