using System.Collections.ObjectModel;

namespace CMiX.MVVM.ViewModels
{
    public interface IComponent
    {
        int ID { get; set; }
        string Name { get; set; }
        bool IsRenaming { get; set; }
        ObservableCollection<Component> Components { get; set; }
        Component CreateAndAddComponent();
        void AddComponent(Component component);
    }
}
