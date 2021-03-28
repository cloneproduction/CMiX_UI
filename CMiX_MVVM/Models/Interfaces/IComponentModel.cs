using System;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.Interfaces
{
    public interface IComponentModel : IModel
    {
        Guid ID { get; set; }
        string Name { get; set; }
        bool IsVisible { get; set; }
        //string Address { get; set; }
        ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}