using System;
using System.Windows;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Modifier : ViewModel
    {
        public Modifier(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor, Beat beat) 
            : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Modifier));

            Counter = new Counter(MessageAddress, oscvalidation, mementor);

            TranslateMode = new GeometryTranslate(MessageAddress + nameof(TranslateMode), oscvalidation, mementor);
            Translate = new Slider(MessageAddress + nameof(Translate), oscvalidation, mementor);
            TranslateBeatModifier = new BeatModifier(MessageAddress, beat, oscvalidation, mementor);

            ScaleMode = new GeometryScale(MessageAddress + nameof(ScaleMode), oscvalidation, mementor);
            Scale = new Slider(MessageAddress + nameof(Scale), oscvalidation, mementor);
            Scale.Amount = 0.25;
            ScaleBeatModifier = new BeatModifier(MessageAddress, beat, oscvalidation, mementor);

            RotationMode = new GeometryRotation(MessageAddress + nameof(RotationMode), oscvalidation, mementor);
            Rotation = new Slider(MessageAddress + nameof(Rotation), oscvalidation, mementor);
            RotationBeatModifier = new BeatModifier(MessageAddress, beat, oscvalidation, mementor);
        }

        public Counter Counter { get; }

        public GeometryTranslate TranslateMode { get; }
        public Slider Translate { get; }
        public BeatModifier TranslateBeatModifier { get; }

        public GeometryScale ScaleMode { get; }
        public Slider Scale { get; }
        public BeatModifier ScaleBeatModifier { get; }

        public GeometryRotation RotationMode { get; }
        public Slider Rotation { get; }
        public BeatModifier RotationBeatModifier { get; }

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            Counter.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Counter)));

            TranslateMode.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateMode)));
            ScaleMode.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(ScaleMode)));
            RotationMode.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(RotationMode)));

            Translate.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Translate)));
            Scale.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Scale)));
            Rotation.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Rotation)));

            TranslateBeatModifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateBeatModifier)));
            ScaleBeatModifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(ScaleBeatModifier)));
            RotationBeatModifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(RotationBeatModifier)));
        }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            ModifierModel modifiermodel = new ModifierModel();
            this.Copy(modifiermodel);
            IDataObject data = new DataObject();
            data.SetData("ModifierModel", modifiermodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ModifierModel"))
            {
                Mementor.BeginBatch();
                DisabledMessages();

                var modifiermodel = data.GetData("ModifierModel") as ModifierModel;
                var geometrymessageaddress = MessageAddress;
                this.Paste(modifiermodel);
                UpdateMessageAddress(geometrymessageaddress);
                this.Copy(modifiermodel);

                EnabledMessages();
                Mementor.EndBatch();

                QueueObjects(modifiermodel);
                SendQueues();
            }
        }

        public void ResetGeometry()
        {
            ModifierModel modifiermodel = new ModifierModel();
            this.Reset();
            this.Copy(modifiermodel);
            QueueObjects(modifiermodel);
            SendQueues();
        }

        public void Copy(ModifierModel modifiermodel)
        {
            modifiermodel.MessageAddress = MessageAddress;
            Counter.Copy(modifiermodel.Counter);
            TranslateMode.Copy(modifiermodel.GeometryTranslate);
            ScaleMode.Copy(modifiermodel.GeometryScale);
            RotationMode.Copy(modifiermodel.GeometryRotation);
            Translate.Copy(modifiermodel.Translate);
            Scale.Copy(modifiermodel.Scale);
            Rotation.Copy(modifiermodel.Rotation);
            TranslateBeatModifier.Copy(modifiermodel.TranslateModifierModel);
            ScaleBeatModifier.Copy(modifiermodel.ScaleModifierModel);
            RotationBeatModifier.Copy(modifiermodel.RotationModifierModel);
        }

        public void Paste(ModifierModel modifiermodel)
        {
            DisabledMessages();

            MessageAddress = modifiermodel.MessageAddress;
            Counter.Copy(modifiermodel.Counter);
            Translate.Paste(modifiermodel.Translate);
            Scale.Paste(modifiermodel.Scale);
            Rotation.Paste(modifiermodel.Rotation);
            TranslateMode.Paste(modifiermodel.GeometryTranslate);
            ScaleMode.Paste(modifiermodel.GeometryScale);
            RotationMode.Paste(modifiermodel.GeometryRotation);
            TranslateBeatModifier.Paste(modifiermodel.TranslateModifierModel);
            ScaleBeatModifier.Paste(modifiermodel.ScaleModifierModel);
            RotationBeatModifier.Paste(modifiermodel.RotationModifierModel);

            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();
            //Mementor.BeginBatch();
            Counter.Reset();
            TranslateMode.Reset();
            ScaleMode.Reset();
            RotationMode.Reset();

            Translate.Reset();
            Scale.Reset();
            Rotation.Reset();

            TranslateBeatModifier.Reset();
            ScaleBeatModifier.Reset();
            RotationBeatModifier.Reset();

            //Mementor.EndBatch();
            EnabledMessages();

            ModifierModel modifiermodel = new ModifierModel();
            this.Copy(modifiermodel);
            QueueObjects(modifiermodel);
            SendQueues();
        }
        #endregion
    }
}
