﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using CMiX.Core.Network.Communicators;
using CMiX.Core.Network.Messages;
using System;

namespace CMiX.Core.Presentation.ViewModels
{
    public class ToggleButton : ViewModel, IControl
    {
        public ToggleButton(ToggleButtonModel toggleButtonModel)
        {
            this.ID = toggleButtonModel.ID;
        }


        public Guid ID { get; set; }
        public Communicator Communicator { get; set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                SetAndNotify(ref _isChecked, value);
                Communicator?.SendMessage(new MessageUpdateViewModel(this));
            }
        }


        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new Communicator(this);
            Communicator.SetCommunicator(communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);
        }


        public IModel GetModel()
        {
            ToggleButtonModel model = new ToggleButtonModel();
            model.ID = this.ID;
            model.IsChecked = this.IsChecked;
            return model;
        }

        public void SetViewModel(IModel model)
        {
            ToggleButtonModel comboBoxModel = model as ToggleButtonModel;
            this.ID = comboBoxModel.ID;
            this.IsChecked = comboBoxModel.IsChecked;
        }
    }
}
