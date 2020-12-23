using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace CMiX.MVVM.ViewModels
{
    public class Instancer : Sender, ITransform, ISubject
    {
        public Instancer(string name, IColleague parentSender, MasterBeat beat) :base(name, parentSender)
        {

            Observers = new List<IObserver>();
            Counter = new Counter(nameof(Counter), this);
            Counter.CounterChangeEvent += Counter_CounterChangeEvent;
            TransformModifierFactory = new TransformModifierFactory(beat);
            Transform = new Transform(nameof(Transform), this);

            //TranslateModifier = new TranslateModifier(nameof(TranslateModifier), this, beat);

            //TranslateModifier = new XYZModifier(nameof(TranslateModifier), this, new Vector3D(0.0, 0.0, 0.0), beat);
            //ScaleModifier = new XYZModifier(nameof(ScaleModifier), this, new Vector3D(1.0, 1.0, 1.0), beat);
            //RotationModifier = new XYZModifier(nameof(RotationModifier), this, new Vector3D(0.0, 0.0, 0.0), beat);

            //UniformScale = new AnimParameter(nameof(UniformScale), this, 1.0, beat);

            //Attach(TranslateModifier);
            //Attach(ScaleModifier);
            //Attach(RotationModifier);
            //Attach(UniformScale);

            NoAspectRatio = false;
            TransformModifiers = new ObservableCollection<ITransformModifier>();

            AddTransformModifierCommand = new RelayCommand(p => AddTransformModifier((TransformModifierNames)p));
        }

        public TransformModifierFactory TransformModifierFactory { get; set; }
        public ICommand AddTransformModifierCommand { get; set; }

        private void Counter_CounterChangeEvent(object sender, CounterEventArgs e)
        {
            Transform.Count = e.Value;
            this.Notify(e.Value);
        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as InstancerModel);
        }

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
                observer.Update(this.Counter.Count);
            }
        }

        private bool _noAspectRatio;
        public bool NoAspectRatio
        {
            get => _noAspectRatio;
            set => SetAndNotify(ref _noAspectRatio, value);
        }

        private List<IObserver> Observers { get; set; }

        public Transform Transform { get; set; }
        public Counter Counter { get; set; }


        private ObservableCollection<ITransformModifier> _transformModifiers;
        public ObservableCollection<ITransformModifier> TransformModifiers
        {
            get => _transformModifiers;
            set => SetAndNotify(ref _transformModifiers, value);
        }


        public void AddTransformModifier(TransformModifierNames transformModifierNames)
        {
            TransformModifiers.Add(TransformModifierFactory.CreateTransformModifier(transformModifierNames, this));
        }

        public void RemoveTransformModifier()
        {

        }
    }
}