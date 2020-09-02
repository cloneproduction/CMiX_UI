using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class XYZModifier : Sendable, IModifier
    {
        public XYZModifier(string name, MasterBeat beat) 
        {
            Name = name;
            X = new AnimParameter(nameof(X), 0.0, beat, false, this);
            Y = new AnimParameter(nameof(Y), 0.0, beat, false, this);
            Z = new AnimParameter(nameof(Z), 0.0, beat, false, this);
        }

        public XYZModifier(string name, MasterBeat beat, Sendable parentSendable) : this(name, beat)
        {
            SubscribeToEvent(parentSendable);
        }

        public override string GetMessageAddress()
        {
            return $"{Name}/";
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as XYZModifierModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }


        private int _isExpanded;
        public int IsExpanded
        {
            get => _isExpanded;
            set => SetAndNotify(ref _isExpanded, value);
        }

        private int _isExpandedX;
        public int IsExpandedX
        {
            get => _isExpandedX;
            set => SetAndNotify(ref _isExpandedX, value);
        }

        private int _isExpandedY;
        public int IsExpandedY
        {
            get => _isExpandedY;
            set => SetAndNotify(ref _isExpandedY, value);
        }

        private int _isExpandedZ;
        public int IsExpandedZ
        {
            get => _isExpandedZ;
            set => SetAndNotify(ref _isExpandedZ, value);
        }

        public string Name { get; set; }
        public AnimParameter X { get; set; }
        public AnimParameter Y { get; set; }
        public AnimParameter Z { get; set; }
    }
}
