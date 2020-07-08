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
            ModeType = ModeType.Steady;
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

        private ModeType _ModeType;
        public ModeType ModeType
        {
            get => _ModeType;
            set
            {
                SetAndNotify(ref _ModeType, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        public IEnumerable<ModeType> ModeTypeSource
        {
            get => Enum.GetValues(typeof(ModeType)).Cast<ModeType>();
        }
    }
}
