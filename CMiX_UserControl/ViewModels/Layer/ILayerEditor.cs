using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
{
    interface ILayerEditor : ILayerContext, IUndoable, ICopyPasteModel<LayerEditorModel>
    {
    }
}