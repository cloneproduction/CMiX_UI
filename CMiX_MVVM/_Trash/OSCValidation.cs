using System;

namespace CMiX.MVVM.ViewModels
{
    public class OSCValidation : ViewModel
    {
        public OSCValidation(OSCSender oscsender)
        {
            OSCSender = oscsender;
            SendEnabled = true;
        }
        private OSCSender _oscsender;
        public OSCSender OSCSender
        {
            get { return _oscsender; }
            set => SetAndNotify(ref _oscsender, value);
        }

        private bool _sendenabled;
        public bool SendEnabled
        {
            get { return _sendenabled; }
            set => SetAndNotify(ref _sendenabled, value);
        }
    }
}