using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class RotationModifier : ViewModel, IControl, IModifier
    {
        public RotationModifier(string name, Rotation rotation, MasterBeat beat, RotationModifierModel rotationModifierModel) 
        {
            //X = new AnimParameter(nameof(X), this, rotation.X.Amount, beat);
            //Y = new AnimParameter(nameof(Y), this, rotation.X.Amount, beat);
            //Z = new AnimParameter(nameof(Z), this, rotation.X.Amount, beat);
        }


        public Guid ID { get; set; }
        public ControlCommunicator Communicator { get; set; }
        public AnimParameter X { get; set; }
        public AnimParameter Y { get; set; }
        public AnimParameter Z { get; set; }


        public void SetCommunicator(Communicator communicator)
        {
            throw new NotImplementedException();
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            throw new NotImplementedException();
        }

        public void SetViewModel(IModel model)
        {
            throw new NotImplementedException();
        }

        public IModel GetModel()
        {
            throw new NotImplementedException();
        }
    }
}