﻿using CMiX.Core.Interfaces;
using CMiX.Core.MessageService;
using CMiX.Core.Models;
using CMiX.Core.Presentation.ViewModels.Beat;
using System;

namespace CMiX.Core.Presentation.ViewModels
{
    public class ScaleModifier : ViewModel, IControl, IModifier
    {
        public ScaleModifier(string name, Scale scale, MasterBeat beat, ScaleModifierModel scaleModifierModel) 
        {
            IsUniform = false;
            //X = new AnimParameter(nameof(X), this, scale.X.Amount, beat);
            //Y = new AnimParameter(nameof(Y), this, scale.Y.Amount, beat);
            //Z = new AnimParameter(nameof(Z), this, scale.Z.Amount, beat);
        }

        //public override void SetReceiver(IMessageReceiver messageReceiver)
        //{
        //    //messageReceiver?.RegisterReceiver(this, ID);
        //}

        public void SetViewModel(IModel model)
        {
            throw new NotImplementedException();
        }

        public IModel GetModel()
        {
            throw new NotImplementedException();
        }

        public void SetCommunicator(Communicator communicator)
        {
            throw new NotImplementedException();
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            throw new NotImplementedException();
        }

        public AnimParameter X { get; set; }
        public AnimParameter Y { get; set; }
        public AnimParameter Z { get; set; }

        private bool _isUniform;
        public bool IsUniform
        {
            get => _isUniform;
            set => SetAndNotify(ref _isUniform, value);
        }
        public ControlCommunicator Communicator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Guid ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}