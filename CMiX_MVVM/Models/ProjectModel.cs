using System.Collections.ObjectModel;

namespace CMiX.MVVM.Models
{
    public class ProjectModel : IComponentModel
    {
        public ProjectModel()
        {
            ComponentModels = new ObservableCollection<IComponentModel>();
        }

        public string MessageAddress { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}