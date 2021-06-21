﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using CMiX.Core.Network.Communicators;
using CMiX.Core.Presentation.ViewModels.Observer;
using System;

namespace CMiX.Core.Presentation.ViewModels
{
    public class Translate : ViewModel, IControl, IObserver
    {
        public Translate(string name, TranslateModel translateModel)
        {
            this.ID = translateModel.ID;
            X = new Slider(nameof(X), translateModel.X);
            Y = new Slider(nameof(Y), translateModel.Y);
            Z = new Slider(nameof(Z), translateModel.Z);
        }


        public Guid ID { get; set; }
        public ControlCommunicator Communicator { get; set; }
        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }


        public void Update(int count)
        {
            //XYZ = new Vector3D[count];
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


        public IModel GetModel()
        {
            TranslateModel model = new TranslateModel();
            model.ID = this.ID;
            model.X = (SliderModel)this.X.GetModel();
            model.Y = (SliderModel)this.Y.GetModel();
            model.Z = (SliderModel)this.Z.GetModel();
            return model;
        }

        public void SetViewModel(IModel model)
        {
            TranslateModel translateModel = model as TranslateModel;
            this.ID = translateModel.ID;
            this.X.SetViewModel(translateModel.X);
            this.Y.SetViewModel(translateModel.Y);
            this.Z.SetViewModel(translateModel.Z);
        }
    }
}