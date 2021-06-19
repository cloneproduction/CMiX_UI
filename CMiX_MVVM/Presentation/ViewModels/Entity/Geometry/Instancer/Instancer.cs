using CMiX.Core.Interfaces;
using CMiX.Core.Models;
using CMiX.Core.Network.Communicators;
using CMiX.Core.Network.Messages;
using CMiX.Core.Presentation.ViewModels.Beat;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.Core.Presentation.ViewModels
{
    public class Instancer : ViewModel, IControl
    {
        public Instancer(MasterBeat beat, InstancerModel instancerModel)
        {
            this.ID = instancerModel.ID;

            Factory = new TransformModifierFactory(beat);
            Transform = new Transform(instancerModel.Transform);

            NoAspectRatio = false;
            TransformModifiers = new ObservableCollection<ITransformModifier>();
            Factory = new TransformModifierFactory(beat);
            CreateTransformModifierCommand = new RelayCommand(p => CreateTransformModifier((TransformModifierNames)p));
            RemoveTransformModifierCommand = new RelayCommand(p => RemoveTransformModifier(p as ITransformModifier));
        }


        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);

            Transform.SetCommunicator(Communicator);
        }
        
        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);

            Transform.UnsetCommunicator(Communicator);
        }


        public Guid ID { get; set; }
        public ControlCommunicator Communicator { get; set; }
        public TransformModifierFactory Factory { get; set; }
        public Transform Transform { get; set; }
        public ICommand CreateTransformModifierCommand { get; set; }
        public ICommand AddTransformModifierCommand { get; set; }
        public ICommand RemoveTransformModifierCommand { get; set; }


        private bool _noAspectRatio;
        public bool NoAspectRatio
        {
            get => _noAspectRatio;
            set => SetAndNotify(ref _noAspectRatio, value);
        }

        private ObservableCollection<ITransformModifier> _transformModifiers;
        public ObservableCollection<ITransformModifier> TransformModifiers
        {
            get => _transformModifiers;
            set => SetAndNotify(ref _transformModifiers, value);
        }


        public ITransformModifier CreateTransformModifier(TransformModifierNames transformModifierNames)
        {
            ITransformModifier transformModifier = Factory.CreateTransformModifier(transformModifierNames);
            AddTransformModifier(transformModifier);
            return transformModifier;
        }

        public ITransformModifier CreateTransformModifier(ITransformModifierModel transformModifierModel)
        {
            ITransformModifier transformModifier = Factory.CreateTransformModifier(transformModifierModel);
            AddTransformModifier(transformModifier);
            return transformModifier;
        }


        public void AddTransformModifier(ITransformModifier transformModifier)
        {
            transformModifier.SetCommunicator(Communicator);
            TransformModifiers.Add(transformModifier);
            Communicator?.SendMessage(new MessageAddTransformModifier(transformModifier.GetModel() as ITransformModifierModel));
        }

        public void RemoveTransformModifier(ITransformModifier transformModifier)
        {
            this.TransformModifiers.Remove(transformModifier);
        }


        public void SetViewModel(IModel model)
        {
            InstancerModel instancerModel = model as InstancerModel;
            this.ID = instancerModel.ID;
            this.Transform.SetViewModel(instancerModel.Transform);
            this.NoAspectRatio = instancerModel.NoAspectRatio;
        }

        public IModel GetModel()
        {
            InstancerModel model = new InstancerModel();
            model.ID = this.ID;
            model.Transform = (TransformModel)this.Transform.GetModel();
            model.NoAspectRatio = this.NoAspectRatio;
            return model;
        }
    }
}