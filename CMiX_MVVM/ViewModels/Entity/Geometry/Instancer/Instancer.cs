using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.Modifiers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels
{
    public class Instancer : Control
    {
        public Instancer(MasterBeat beat, InstancerModel instancerModel)
        {
            this.ID = instancerModel.ID;

            Factory = new TransformModifierFactory(beat);
            Transform = new Transform(instancerModel.Transform);

            NoAspectRatio = false;
            TransformModifiers = new ObservableCollection<ITransformModifier>();

            CreateTransformModifierCommand = new RelayCommand(p => CreateTransformModifier((TransformModifierNames)p));
            RemoveTransformModifierCommand = new RelayCommand(p => RemoveTransformModifier(p as ITransformModifier));
        }


        public override void SetReceiver(IMessageReceiver messageReceiver)
        {
            var messageProcessor = new InstancerMessageProcessor(this);
            MessageReceiver = new MessageReceiver(messageProcessor);
            messageReceiver.RegisterReceiver(MessageReceiver);
        }


        public ICommand CreateTransformModifierCommand { get; set; }
        public ICommand AddTransformModifierCommand { get; set; }
        public ICommand RemoveTransformModifierCommand { get; set; }
        public TransformModifierFactory Factory { get; set; }


        private bool _noAspectRatio;
        public bool NoAspectRatio
        {
            get => _noAspectRatio;
            set => SetAndNotify(ref _noAspectRatio, value);
        }

        public Transform Transform { get; set; }


        private ObservableCollection<ITransformModifier> _transformModifiers;
        public ObservableCollection<ITransformModifier> TransformModifiers
        {
            get => _transformModifiers;
            set => SetAndNotify(ref _transformModifiers, value);
        }


        public void CreateTransformModifier(TransformModifierNames transformModifierNames)
        {
            ITransformModifier transformModifier = Factory.CreateTransformModifier(transformModifierNames);
            transformModifier.SetSender(MessageSender);
            AddTransformModifier(transformModifier);
        }


        public void AddTransformModifier(ITransformModifier transformModifier)
        {
            TransformModifiers.Add(transformModifier);
            MessageSender?.SendMessage(new MessageAddTransformModifier(transformModifier.GetModel() as ITransformModifierModel));
        }


        public void RemoveTransformModifier(ITransformModifier transformModifier)
        {
            this.TransformModifiers.Remove(transformModifier);
        }


        public void UpdateOnBeatTick(double period)
        {
            for (int i = TransformModifiers.Count - 1; i >= 0; i--)
            {
                if (TransformModifiers[i].SelectedModifierType == ModifierType.GROUP)
                {

                }
            }
        }


        public override void SetViewModel(IModel model)
        {
            InstancerModel instancerModel = model as InstancerModel;
            this.ID = instancerModel.ID;
            this.Transform.SetViewModel(instancerModel.Transform);
            this.NoAspectRatio = instancerModel.NoAspectRatio;
        }

        public override IModel GetModel()
        {
            InstancerModel model = new InstancerModel();
            model.ID = this.ID;
            model.Transform = (TransformModel)this.Transform.GetModel();
            model.NoAspectRatio = this.NoAspectRatio;
            return model;
        }
    }
}