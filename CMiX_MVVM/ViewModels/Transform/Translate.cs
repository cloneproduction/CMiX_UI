﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using CMiX.MVVM.ViewModels.Observer;

namespace CMiX.MVVM.ViewModels
{
    public class Translate : MessageCommunicator, IObserver
    {
        public Translate(string name, IMessageProcessor parentSender, TranslateModel translateModel) : base (name, parentSender)
        {
            X = new Slider(nameof(X), this, translateModel.X);
            Y = new Slider(nameof(Y), this, translateModel.Y);
            Z = new Slider(nameof(Z), this, translateModel.Z);
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }

        public void Update(int count)
        {
            //XYZ = new Vector3D[count];
        }

        public override IModel GetModel()
        {
            TranslateModel model = new TranslateModel();
            model.X = (SliderModel)this.X.GetModel();
            model.Y = (SliderModel)this.Y.GetModel();
            model.Z = (SliderModel)this.Z.GetModel();
            return model;
        }

        public override void SetViewModel(IModel model)
        {
            TranslateModel translateModel = model as TranslateModel;
            this.X.SetViewModel(translateModel.X);
            this.Y.SetViewModel(translateModel.Y);
            this.Z.SetViewModel(translateModel.Z);
        }
    }
}