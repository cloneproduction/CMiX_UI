using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.ViewModels
{
    public interface ISenderTest
    {
        void SetViewModel(IModel model);
        IModel GetModel();
    }
}