using CMiX.MVVM;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
{
    interface ILayerEditor : ILayerContext, ISendable, ICopyPasteModel, IUndoable
    {
    }
}