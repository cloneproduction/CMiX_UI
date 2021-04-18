﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels
{
    public class Inverter : MessageCommunicator
    {
        public Inverter(string name, InverterModel inverterModel) 
        {
            Invert = new Slider(nameof(Invert), inverterModel.Invert);
            InvertMode = inverterModel.InvertMode;
        }

        public override void SetModuleReceiver(ModuleMessageReceiver messageDispatcher)
        {
            messageDispatcher.RegisterMessageReceiver(this);
            Invert.SetModuleReceiver(messageDispatcher);
        }

        public Slider Invert { get; set; }

        private string _invertMode;
        public string InvertMode
        {
            get => _invertMode;
            set
            {
                SetAndNotify(ref _invertMode, value);

            }
        }

        public override void SetViewModel(IModel model)
        {
            InverterModel inverterModel = model as InverterModel;
            this.Invert.SetViewModel(inverterModel.Invert);
            this.InvertMode = inverterModel.InvertMode;
        }

        public override IModel GetModel()
        {
            InverterModel model = new InverterModel();
            model.Invert = (SliderModel)this.Invert.GetModel();
            model.InvertMode = this.InvertMode;
            return model;
        }
    }
}