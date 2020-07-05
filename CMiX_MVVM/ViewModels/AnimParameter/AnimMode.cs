using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMiX.MVVM.ViewModels
{
    public class AnimMode : Sendable
    {
        public AnimMode()
        {
            Mode = AnimModeEnum.None;
        }
        public AnimMode(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as AnimModeModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        private AnimModeEnum _Mode;
        public AnimModeEnum Mode
        {
            get => _Mode;
            set
            {
                SetAndNotify(ref _Mode, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        public IEnumerable<AnimModeEnum> AnimModeSource
        {
            get => Enum.GetValues(typeof(AnimModeEnum)).Cast<AnimModeEnum>();
        }
    }
}
