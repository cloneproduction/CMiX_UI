using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class RotationModifier : Control, IModifier
    {
        public RotationModifier(string name, Rotation rotation, MasterBeat beat, RotationModifierModel rotationModifierModel) 
        {
            //X = new AnimParameter(nameof(X), this, rotation.X.Amount, beat);
            //Y = new AnimParameter(nameof(Y), this, rotation.X.Amount, beat);
            //Z = new AnimParameter(nameof(Z), this, rotation.X.Amount, beat);
        }

        public override void SetViewModel(IModel model)
        {
            throw new NotImplementedException();
        }

        public override IModel GetModel()
        {
            throw new NotImplementedException();
        }

        //public override void SetReceiver(ModuleReceiver messageReceiver)
        //{
        //    //messageReceiver?.RegisterReceiver(this, ID);
        //}

        public AnimParameter X { get; set; }
        public AnimParameter Y { get; set; }
        public AnimParameter Z { get; set; }
    }
}