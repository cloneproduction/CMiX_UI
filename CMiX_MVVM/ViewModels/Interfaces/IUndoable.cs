using Memento;

namespace CMiX.MVVM.ViewModels
{
    public interface IUndoable
    {
        Mementor Mementor { get; set; }
    }
}
