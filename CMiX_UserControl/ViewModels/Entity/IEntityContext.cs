using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels
{
    public interface IEntityContext
    {
        ObservableCollection<Entity> Entities { get; set; }
        Entity SelectedEntity { get; set; }
    }
}