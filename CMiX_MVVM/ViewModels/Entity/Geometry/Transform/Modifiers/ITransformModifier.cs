using System;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.ViewModels
{
    public interface ITransformModifier : IDisposable
    {
        string Name { get; set; }
        int ID { get; set; }
        ObservableCollection<Transform> Transforms { get; set; }
        ModifierType SelectedModifierType { get; set; }
        void UpdateOnBeatTick(double period);
        void UpdateOnGameLoop(double period);
    }
}