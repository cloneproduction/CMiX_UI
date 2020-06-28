using System.Windows;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Instancer : Sendable, ITransform
    {
        public Instancer(Beat beat)
        {
            Transform = new Transform(this);
            Counter = new Counter(this);
            TranslateModifier = new TranslateModifier(beat, this);
            ScaleModifier = new ScaleModifier(beat, this);
            RotationModifier = new RotationModifier(beat, this);

            NoAspectRatio = false;
        }

        public Instancer(Beat beat, Sendable parentSendable) : this(beat)
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as InstancerModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public Transform Transform { get; set; }
        public Counter Counter { get; set; }
        public TranslateModifier TranslateModifier { get; set; }
        public ScaleModifier ScaleModifier { get; set; }
        public RotationModifier RotationModifier { get; set; }

        private bool _noAspectRatio;
        public bool NoAspectRatio
        {
            get => _noAspectRatio;
            set
            {
                //if (Mementor != null)
                //    Mementor.PropertyChange(this, "NoAspectRatio");
                SetAndNotify(ref _noAspectRatio, value);
                //SendMessages(MessageAddress + nameof(NoAspectRatio), NoAspectRatio.ToString());
            }
        }

        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("InstancerModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("InstancerModel"))
            {
                var instancermodel = data.GetData(nameof(InstancerModel)) as InstancerModel;
                this.SetViewModel(instancermodel);
            }
        }
    }
}
