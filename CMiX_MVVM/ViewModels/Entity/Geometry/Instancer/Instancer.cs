using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Instancer : Sender
    {
        public Instancer(string name, IColleague parentSender, MasterBeat beat) : base (name, parentSender)
        {
            Factory = new TransformModifierFactory(beat);
            Transform = new Transform(nameof(Transform), this);

            NoAspectRatio = false;
            TransformModifiers = new ObservableCollection<TransformModifier>();

            AddTransformModifierCommand = new RelayCommand(p => AddTransformModifier((TransformModifierNames)p));
            RemoveTransformModifierCommand = new RelayCommand(p => RemoveTransformModifier(p as TransformModifier));
        }

        public ICommand AddTransformModifierCommand { get; set; }
        public ICommand RemoveTransformModifierCommand { get; set; }

        private TransformModifierFactory Factory { get; set; }

        public override void Receive(Message message)
        {
            switch (message.Command)
            {
                case MessageCommand.ADD_TRANSFORMMODIFIER:
                    {
                        var model = message.Obj as ITransformModifierModel;
                        TransformModifierNames name = (TransformModifierNames)message.CommandParameter;
                        var component = Factory.CreateTransformModifier(name, model, this);
                        this.TransformModifiers.Add(component);
                        break;
                    }
                case MessageCommand.REMOVE_COMPONENT:
                    {
                        int index = (int)message.Obj;
                        this.TransformModifiers[index].Dispose();
                        this.TransformModifiers.RemoveAt(index);
                        break;
                    }
                case MessageCommand.UPDATE_VIEWMODEL:
                    {
                        var model = message.Obj as InstancerModel;
                        this.SetViewModel(model);
                        break;
                    }
                default: break;
            }
        }

        private bool _noAspectRatio;
        public bool NoAspectRatio
        {
            get => _noAspectRatio;
            set => SetAndNotify(ref _noAspectRatio, value);
        }

        public Transform Transform { get; set; }


        private ObservableCollection<TransformModifier> _transformModifiers;
        public ObservableCollection<TransformModifier> TransformModifiers
        {
            get => _transformModifiers;
            set => SetAndNotify(ref _transformModifiers, value);
        }

        public void AddTransformModifier(TransformModifierNames transformModifierNames)
        {
            var transformModifier = Factory.CreateTransformModifier(transformModifierNames, this);
            TransformModifiers.Add(transformModifier);
            Message message = new Message(MessageCommand.ADD_TRANSFORMMODIFIER, Address, transformModifier.GetModel(), transformModifierNames);
            this.Send(message);
        }

        public void RemoveTransformModifier(TransformModifier transformModifier)
        {
            transformModifier.Dispose();
            this.TransformModifiers.Remove(transformModifier);
        }
    }
}