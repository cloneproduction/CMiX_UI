using System;
using System.Diagnostics;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class LFO : Sendable, IAnimMode
    {
        public LFO()
        {

        }

        public LFO(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {

        }

        private bool _IsEnabled;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set => SetAndNotify(ref _IsEnabled, value);
        }

        private bool _invert;
        public bool Invert
        {
            get => _invert;
            set => SetAndNotify(ref _invert, value);
        }
    }
}