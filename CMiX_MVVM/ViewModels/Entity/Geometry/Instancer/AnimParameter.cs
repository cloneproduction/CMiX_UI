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

        #region PROPERTIES
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
        #endregion

        #region COPY/PASTE/RESET
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
                //Mementor.BeginBatch();
                var animparametermodel = data.GetData("AnimParameterModel") as AnimParameterModel;
                this.Paste(animparametermodel);

                //Mementor.EndBatch();
                //SendMessages(MessageAddress, GetModel());
                //QueueObjects(animparametermodel);
                //SendQueues();
            }
        }

        public void ResetGeometry()
        {
            this.Reset();
            //SendMessages(MessageAddress, GetModel());
            //QueueObjects(animparametermodel);
            //SendQueues();
        }



        //public void Copy(AnimParameterModel animparametermodel)
        //{
        //    Slider.CopyModel(animparametermodel.Slider);
        //    BeatModifier.CopyModel(animparametermodel.BeatModifier);
        //}

        public void Paste(AnimParameterModel animparametermodel)
        {
            Slider.SetViewModel(animparametermodel.Slider);
            BeatModifier.SetViewModel(animparametermodel.BeatModifier);
        }

        public void Reset()
        {
            Slider.Reset();
            BeatModifier.Reset();


            AnimParameterModel animparametermodel = this.GetModel();
            //this.Copy(animparametermodel);
            //SendMessages(MessageAddress, GetModel());
            //QueueObjects(animparametermodel);
            //SendQueues();
        }
        #endregion
    }
}
