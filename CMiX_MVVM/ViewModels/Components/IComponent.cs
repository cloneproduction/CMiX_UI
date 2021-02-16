using System;
using System.Collections.ObjectModel;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.Components.Factories;

namespace CMiX.MVVM.ViewModels
{
    public interface IComponent :  IDisposable
    {
        int ID { get; set; }
        string Name { get; set; }
        bool IsRenaming { get; set; }
        bool IsVisible { get; set; }
        bool IsSelected { get; set; }

        IComponentFactory ComponentFactory { get; set; }
        ObservableCollection<IComponent> Components { get; set; }
        Component CreateAndAddComponent();

        void SetViewModel(IModel model);

        IModel GetModel();

        void SetVisibility();
        void AddComponent(IComponent component);
        void RemoveComponent(Component component);
        string GetAddress();
    }
}
