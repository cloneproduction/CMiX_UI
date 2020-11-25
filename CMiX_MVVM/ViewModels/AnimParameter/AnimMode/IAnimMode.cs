using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public interface IAnimMode
    {
        void UpdateOnBeatTick(AnimParameter animParameter, double period);
        void UpdateParameters(AnimParameter animParameter, double period);
    }
}