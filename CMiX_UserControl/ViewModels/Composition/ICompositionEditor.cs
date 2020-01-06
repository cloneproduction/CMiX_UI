using CMiX.MVVM;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
{ 
    public interface ICompositionEditor : ICompositionContext, ISendable, ICopyPasteModel, IUndoable
    {
    }
}