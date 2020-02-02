using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using System.Windows.Input;

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

        void AddComponent(IComponent component);
        void RemoveComponent(IComponent component);
    }
}