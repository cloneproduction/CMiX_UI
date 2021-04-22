using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.MessageService.Messages;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels
{
    public class Instancer : Module
    {
        public Instancer(MasterBeat beat, InstancerModel instancerModel) 
        {
            Factory = new TransformModifierFactory(beat);
            Transform = new Transform(instancerModel.Transform);

            NoAspectRatio = false;
            TransformModifiers = new ObservableCollection<ITransformModifier>();

            AddTransformModifierCommand = new RelayCommand(p => AddTransformModifier((TransformModifierNames)p));
            RemoveTransformModifierCommand = new RelayCommand(p => RemoveTransformModifier(p as ITransformModifier));
        }

        public override void SetReceiver(IMessageReceiver<Module> messageReceiver)
        {
            messageReceiver?.RegisterReceiver(this, ID);
            Transform.SetReceiver(messageReceiver);
        }


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

        public void AddTransformModifier(TransformModifierNames transformModifierNames)
        {
            var transformModifier = Factory.CreateTransformModifier(transformModifierNames, this);
            TransformModifiers.Add(transformModifier);
            var message = new MessageAddTransformModifier(this.ID, transformModifierNames, transformModifier.GetModel() as ITransformModifierModel);
            //this.MessageDispatcher.NotifyOut(message);
        }


        public void RemoveTransformModifier(ITransformModifier transformModifier)
        {
            //transformModifier.Dispose();
            this.TransformModifiers.Remove(transformModifier);
        }


        public void UpdateOnBeatTick(double period)
        {
            for (int i = TransformModifiers.Count - 1; i >= 0; i--)
            {
                if(TransformModifiers[i].SelectedModifierType == ModifierType.GROUP)
                {

                }
            }
        }

        public override void SetViewModel(IModel model)
        {
            InstancerModel instancerModel = model as InstancerModel;
            this.Transform.SetViewModel(instancerModel.Transform);
            this.NoAspectRatio = instancerModel.NoAspectRatio;
        }

        public override IModel GetModel()
        {
            InstancerModel model = new InstancerModel();
            model.Transform = (TransformModel)this.Transform.GetModel();
            model.NoAspectRatio = this.NoAspectRatio;
            return model;
        }
    }
}