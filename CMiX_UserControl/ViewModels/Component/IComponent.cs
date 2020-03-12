using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.MVVM.Resources;
using System.Windows;

namespace CMiX.Studio.ViewModels
{
    public interface IComponent
    {
        bool IsRenaming { get; set; }
        int ID { get; set; }
        string Name { get; set; }
        string MessageAddress { get; set; }
        bool IsSelected { get; set; }

        Assets Assets { get; set; }
        Mementor Mementor { get; set; }
        Beat Beat { get; set; }
        MessageService MessageService { get; set; }
        ICommand RenameCommand { get; }

        ObservableCollection<IComponent> Components { get; set; }

        void AddComponent(IComponent component);
        void RemoveComponent(IComponent component);
    }
}