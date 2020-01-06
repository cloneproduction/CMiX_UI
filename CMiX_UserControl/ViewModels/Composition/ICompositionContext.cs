using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels
{
    public interface ICompositionContext : ISendable, IUndoable
    {
        Assets Assets { get; set; }
        ObservableCollection<Composition> Compositions { get; set; }
        Composition SelectedComposition { get; set; }
    }
}