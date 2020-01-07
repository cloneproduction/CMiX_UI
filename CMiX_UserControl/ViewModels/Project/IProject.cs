using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;

namespace CMiX.Studio.ViewModels
{
    public interface IProject : ICopyPasteModel<ProjectModel>, IUndoable
    {
    }
}