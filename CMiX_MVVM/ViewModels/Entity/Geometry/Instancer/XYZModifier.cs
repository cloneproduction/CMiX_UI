using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using CMiX.MVVM.ViewModels.Observer;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace CMiX.MVVM.ViewModels
{
    public class XYZModifier : Sender, IModifier, ISubject, IObserver
    {
        public XYZModifier(string name, Sender parentSender, Vector3D vector3D, MasterBeat beat) : base (name, parentSender)
        {
            Name = name;
            Observers = new List<IObserver>();

            //X = new AnimParameter(nameof(X), this, vector3D.X, beat);
            //Y = new AnimParameter(nameof(Y), this, vector3D.Y, beat);
            //Z = new AnimParameter(nameof(Z), this, vector3D.Z, beat);

            Attach(X);
            Attach(Y);
            Attach(Z);
        }

        public override void Receive(IMessage message)
        {
            this.SetViewModel(message.Obj as XYZModifierModel);
        }

        private List<IObserver> Observers { get; set; }

        public void Attach(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void Notify(int count)
        {
            foreach (var observer in Observers)
            {
                observer.Update(count);
            }
        }

        public void Update(int count)
        {
            foreach (var observer in Observers)
            {
                observer.Update(count);
            }
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

        private string _name;
        public string Name
        {
            get => _name;
            set => SetAndNotify(ref _name, value);
        }

        public AnimParameter X { get; set; }
        public AnimParameter Y { get; set; }
        public AnimParameter Z { get; set; }
    }
}
