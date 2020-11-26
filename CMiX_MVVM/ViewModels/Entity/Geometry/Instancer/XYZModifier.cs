using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class XYZModifier : Sender, IModifier
    {
        public XYZModifier(string name, IColleague parentSender, MasterBeat beat, Counter counter) : base (name, parentSender)
        {
            Name = name;
            X = new AnimParameter(nameof(X), this, 0.0, counter, beat, false);
            Y = new AnimParameter(nameof(Y), this, 0.0, counter, beat, false);
            Z = new AnimParameter(nameof(Z), this, 0.0, counter, beat, false);
        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as XYZModifierModel);
            System.Console.WriteLine("Received XYZModifier");
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
