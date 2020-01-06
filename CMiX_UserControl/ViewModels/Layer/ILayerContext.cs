using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using System.Collections.ObjectModel;

namespace CMiX.Studio.ViewModels
{
    public interface ILayerContext : ISendable, IUndoable
    {
        MasterBeat MasterBeat { get; set; }
        Assets Assets { get; set; }
        ObservableCollection<Layer> Layers { get; set; }
        Layer SelectedLayer { get; set; }
    }
}