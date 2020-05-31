using System;
using System.Windows.Input;
using System.Windows;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM;

namespace CMiX.MVVM.ViewModels
{
    public class Coloration : Sendable
    {
        #region CONSTRUCTORS
        public Coloration(Beat beat) 
        {
            BeatModifier = new BeatModifier(beat);
            ColorSelector = new ColorSelector(this);

            Hue = new RangeControl(nameof(Hue));
            Saturation = new RangeControl(nameof(Saturation));
            Value = new RangeControl(nameof(Value));

            CopyColorationCommand = new RelayCommand(p => CopyColoration());
            PasteColorationCommand = new RelayCommand(p => PasteColoration());
            ResetColorationCommand = new RelayCommand(p => ResetColoration());
            ResetCommand = new RelayCommand(p => Reset());
        }


        #endregion
        public Coloration(Beat beat, Sendable parentSendable) : this(beat)
        {
            SubscribeToEvent(parentSendable);
        }


        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as ColorationModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        #region PROPERTIES
        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand CopyColorationCommand { get; }
        public ICommand PasteColorationCommand { get; }
        public ICommand ResetColorationCommand { get; }

        public ColorSelector ColorSelector { get; }
        public BeatModifier BeatModifier { get; }
        public RangeControl Hue { get; }
        public RangeControl Saturation { get; }
        public RangeControl Value { get; }
        #endregion

        #region COPY/PASTE/RESET



        public void Reset()
        {
            ColorSelector.Reset();
            BeatModifier.Reset();
            Hue.Reset();
            Saturation.Reset();
            Value.Reset();
        }

        public void CopyColoration()
        {
            IDataObject data = new DataObject();
            data.SetData("ColorationModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteColoration()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ColorationModel"))
            {
                //this.Mementor.BeginBatch();

                var colorationmodel = data.GetData("ColorationModel") as ColorationModel;
                this.SetViewModel(colorationmodel);


                //this.Mementor.EndBatch();

                //SendMessages(nameof(ColorationModel), GetModel());
            }
        }

        public void ResetColoration()
        {
            this.Reset();
            //SendMessages(nameof(Coloration), GetModel());
        }
        #endregion
    }
}