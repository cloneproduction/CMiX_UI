using System;
using System.Collections.ObjectModel;

namespace CMiX.Core.Interfaces
{
    public interface IComponentModel : IModel
    {
        Guid ID { get; set; }
        string Name { get; set; }
        bool IsVisible { get; set; }
        ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}