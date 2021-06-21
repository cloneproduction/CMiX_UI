// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using CMiX.Core.Network.Communicators;
using System;

namespace CMiX.Core.Presentation.ViewModels
{
    public class Scale : ViewModel, IControl
    {
        public Scale(string name, ScaleModel scaleModel)
        {
            this.ID = scaleModel.ID;
            Uniform = new Slider(nameof(Uniform), scaleModel.Uniform);
            Uniform.Amount = 1.0;

            X = new Slider(nameof(X), scaleModel.X);
            X.Amount = 1.0;

            Y = new Slider(nameof(Y), scaleModel.Y);
            Y.Amount = 1.0;

            Z = new Slider(nameof(Z), scaleModel.Z);
            Z.Amount = 1.0;

            IsUniform = true;
        }


        public Guid ID { get; set; }
        public ControlCommunicator Communicator { get; set; }
        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }
        public Slider Uniform { get; set; }

        private bool _isUniform;
        public bool IsUniform
        {
            get => _isUniform;
            set => SetAndNotify(ref _isUniform, value);
        }


        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);

            X.SetCommunicator(Communicator);
            Y.SetCommunicator(Communicator);
            Z.SetCommunicator(Communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);

            X.UnsetCommunicator(Communicator);
            Y.UnsetCommunicator(Communicator);
            Z.UnsetCommunicator(Communicator);
        }


        public void SetViewModel(IModel model)
        {
            ScaleModel scaleModel = new ScaleModel();
            this.ID = scaleModel.ID;
            this.X.SetViewModel(scaleModel.X);
            this.Y.SetViewModel(scaleModel.Y);
            this.Z.SetViewModel(scaleModel.Z);
            this.Uniform.SetViewModel(scaleModel.Uniform);
        }

        public IModel GetModel()
        {
            ScaleModel model = new ScaleModel();
            model.ID = this.ID;
            model.X = (SliderModel)this.X.GetModel();
            model.Y = (SliderModel)this.Y.GetModel();
            model.Z = (SliderModel)this.Z.GetModel();
            model.Uniform = (SliderModel)this.Uniform.GetModel();
            return model;
        }
    }
}
