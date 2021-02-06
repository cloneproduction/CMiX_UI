using CMiX.MVVM.ViewModels.Components.Factories;
using System;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.ViewModels
{
    public interface IComponent : IDisposable
    {
        int ID { get; set; }
        string Name { get; set; }
        bool IsRenaming { get; set; }
        bool IsVisible { get; set; }
        bool IsSelected { get; set; }

        ComponentFactory ComponentFactory { get; set; }
        ObservableCollection<IComponent> Components { get; set; }
        Component CreateAndAddComponent();

        void AddComponent(Component component);
        string GetAddress();
        void SetVisibility();
    }
}
