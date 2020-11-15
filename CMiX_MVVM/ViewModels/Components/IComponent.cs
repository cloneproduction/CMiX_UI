using CMiX.MVVM.ViewModels.Components.Factories;
using CMiX.Studio.ViewModels.MessageService;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.ViewModels
{
    public interface IComponent
    {
        int ID { get; set; }
        string Name { get; set; }
        bool IsRenaming { get; set; }
        ObservableCollection<Component> Components { get; set; }
        MessengerManager MessengerManager { get; set; }
        Component CreateAndAddChild();
        void AddComponent(Component component);
    }
}
