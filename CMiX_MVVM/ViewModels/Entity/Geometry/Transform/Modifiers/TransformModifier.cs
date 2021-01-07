using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;

namespace CMiX.MVVM.ViewModels
{
    public abstract class TransformModifier : Sender, ITransformModifier
    {
        public TransformModifier(string name, IColleague parentSender, int id) : base (name, parentSender)
        {
            this.ID = id;
            this.Address = $"{this.GetType().Name}/{ID}/";
            this.MessageMediator = parentSender.MessageMediator;
            this.Transforms = new ObservableCollection<Transform>();
        }

        public string Name { get; set; }
        public int ID { get; set; }
        public ObservableCollection<Transform> Transforms { get; set; }
        public Vector3D[] Location { get; set; }
        public Vector3D[] Scale { get; set; }
        public Vector3D[] Rotation { get; set; }

        public abstract void UpdateOnBeatTick(double period, ITransformModifier transformModifier);
        public abstract void UpdateOnGameLoop(double period, ITransformModifier transformModifier);

        public override void Receive(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
