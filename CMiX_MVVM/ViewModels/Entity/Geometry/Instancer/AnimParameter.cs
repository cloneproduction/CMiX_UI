using System;
using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class AnimParameter : ViewModel
    {
        public AnimParameter(string name, Beat beat, bool isEnabled)
        {
            Mode = AnimMode.None;
            IsEnabled = isEnabled;
            Slider = new Slider(nameof(Slider));
            BeatModifier = new BeatModifier(beat);
        }

        private bool _IsEnabled;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set
            {
                //if (Mementor != null)
                //    Mementor.PropertyChange(this, nameof(IsEnabled));
                SetAndNotify(ref _IsEnabled, value);
                //SendMessages(MessageAddress + nameof(Mode), IsEnabled);
            }
        }

        private AnimMode _Mode;
        public AnimMode Mode
        {
            get => _Mode;
            set
            {
                //if (Mementor != null)
                //    Mementor.PropertyChange(this, nameof(Mode));
                SetAndNotify(ref _Mode, value);
                //SendMessages(MessageAddress + nameof(Mode), Mode);
            }
        }

        public Slider Slider { get; set; }
        public BeatModifier BeatModifier { get; set; }

        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("AnimParameterModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("AnimParameterModel"))
            {
                var animparametermodel = data.GetData("AnimParameterModel") as AnimParameterModel;
                this.SetViewModel(animparametermodel);
            }
        }


        public void Paste(AnimParameterModel animparametermodel)
        {
            Slider.SetViewModel(animparametermodel.Slider);
            BeatModifier.SetViewModel(animparametermodel.BeatModifier);
        }
    }
}
