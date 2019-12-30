using System.Collections.ObjectModel;

namespace CMiX.ViewModels
{
    public interface IEntityContext
    {
        ObservableCollection<Entity> Entities { get; set; }
        Entity SelectedEntity { get; set; }
    }
}