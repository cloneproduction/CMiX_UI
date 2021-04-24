using CMiX.MVVM.ViewModels.MessageService;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.ViewModels
{
    public interface ITransformModifier
    {
        string Name { get; set; }
        ObservableCollection<Transform> Transforms { get; set; }
        ModifierType SelectedModifierType { get; set; }
        void UpdateOnBeatTick(double period);
        void UpdateOnGameLoop(double period);
    }
}