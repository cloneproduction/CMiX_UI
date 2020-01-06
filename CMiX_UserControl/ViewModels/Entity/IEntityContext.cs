using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels
{
    public interface IEntityContext : ISendable, IUndoable
    {
        Beat Beat { get; set; }
        Assets Assets { get; set; }
        ObservableCollection<Entity> Entities { get; set; }
        Entity SelectedEntity { get; set; }
    }
}