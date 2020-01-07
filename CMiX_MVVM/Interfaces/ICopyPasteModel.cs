using CMiX.MVVM.Models;

namespace CMiX.MVVM
{
    public interface ICopyPasteModel <T>
    {
        void CopyModel(T model);
        void PasteModel(T model);
    }
}