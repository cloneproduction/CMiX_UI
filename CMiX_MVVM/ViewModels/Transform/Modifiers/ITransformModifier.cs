using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.ViewModels
{
    public interface ITransformModifier
    {
        TransformModifierNames Name { get; set; }
        ObservableCollection<Transform> Transforms { get; set; }
        ModifierType SelectedModifierType { get; set; }

        void UpdateOnBeatTick(double period);
        void UpdateOnGameLoop(double period);

        void SetReceiver(IMessageReceiver messageReceiver);
        void SetSender(IMessageSender messageSender);

        IModel GetModel();
        void SetViewModel(IModel model);
    }
}