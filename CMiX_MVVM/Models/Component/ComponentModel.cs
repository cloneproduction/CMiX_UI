using System.Collections.ObjectModel;

namespace CMiX.MVVM.Models
{
    public class ComponentModel : Model, IComponentModel
    {
        public ComponentModel()
        {
            ComponentModels = new ObservableCollection<ComponentModel>();
        }

        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public bool IsVisible { get; set; }
        public bool ParentIsVisible { get; set; }
        public bool IsExpanded { get; set; }

        public ObservableCollection<ComponentModel> ComponentModels { get; set; }
        public ComponentModel SelectedComponent { get; set; }
        public int ID { get; set; }
        public string MessageAddress { get; set; }
        ObservableCollection<ComponentModel> ComponentModels { get; set; }
    }
}
