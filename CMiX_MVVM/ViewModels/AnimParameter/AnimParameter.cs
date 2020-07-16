using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMiX.MVVM.ViewModels
{
    public class AnimParameter : Sendable
    {
        public AnimParameter(string name, MasterBeat beat, bool isEnabled = true)
        {
;           BeatModifier = new BeatModifier(beat, this);
            BeatModifier.BeatTap += BeatModifier_BeatTap;
            SelectedModeType = ModeType.Steady;
            Name = name;
            IsEnabled = isEnabled;
        }

        public AnimParameter(string name, MasterBeat beat, bool isEnabled, Sendable parentSendable) : this(name, beat, isEnabled)
        {
            SubscribeToEvent(parentSendable);
            
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as AnimParameterModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        private void BeatModifier_BeatTap(object sender, EventArgs e)
        {
            AnimMode.Update();
        }



        public override string GetMessageAddress()
        {
            return $"{this.GetType().Name}/{Name}/";
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        private bool _IsEnabled;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set
            {
                SetAndNotify(ref _IsEnabled, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        private ModeType _selectedModeType;
        public ModeType SelectedModeType
        {
            get => _selectedModeType;
            set
            {
                SetAndNotify(ref _selectedModeType, value);
                SetAnimMode();
                //OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        public IEnumerable<ModeType> ModeTypeSource
        {
            get => Enum.GetValues(typeof(ModeType)).Cast<ModeType>();
        }

        private void SetAnimMode()
        {
            AnimMode = ModesFactory.CreateMode(SelectedModeType);
        }

        private IAnimMode _animMode;
        public IAnimMode AnimMode
        {
            get => _animMode;
            set => SetAndNotify(ref _animMode, value);
        }

        public BeatModifier BeatModifier { get; set; }
        public EasingType EasingType { get; set; }
    }
}
