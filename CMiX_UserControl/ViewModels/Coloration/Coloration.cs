using System;
using System.Windows.Input;
using System.Windows;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM;

namespace CMiX.Studio.ViewModels
{
    public class Coloration : ViewModel
    {
        #region CONSTRUCTORS
        public Coloration(string messageAddress, Beat beat) 
        {
            MessageAddress = $"{messageAddress}{nameof(Coloration)}/";

            BeatModifier = new BeatModifier(MessageAddress, beat);
            ColorSelector = new ColorSelector(MessageAddress);

            Hue = new RangeControl(MessageAddress + nameof(Hue));
            Saturation = new RangeControl(MessageAddress + nameof(Saturation));
            Value = new RangeControl(MessageAddress + nameof(Value));

            CopyColorationCommand = new RelayCommand(p => CopyColoration());
            PasteColorationCommand = new RelayCommand(p => PasteColoration());
            ResetColorationCommand = new RelayCommand(p => ResetColoration());
            ResetCommand = new RelayCommand(p => Reset());
        }
        #endregion

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
        public string MessageAddress { get; set; }
        public MessengerService MessengerService { get; set; }
        #endregion

        #region COPY/PASTE/RESET



        public void Reset()
        {
            MessengerService.Disable();

            ColorSelector.Reset();
            BeatModifier.Reset();
            Hue.Reset();
            Saturation.Reset();
            Value.Reset();

            MessengerService.Enable();
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