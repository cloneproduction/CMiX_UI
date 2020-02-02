using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels
{
    public class Component : ViewModel
    {
        public Component()
        {

        }

        public ObservableCollection<IComponent> Components { get; set; }
        public IComponent SelectedComponent { get; set; }
    }
}