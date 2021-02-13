﻿using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.MessageService.Messages;

namespace CMiX.MVVM.ViewModels
{
    public class Instancer : Sender
    {
        public Instancer(string name, IMessageProcessor parentSender, MasterBeat beat) : base (name, parentSender)
        {
            Factory = new TransformModifierFactory(beat);
            Transform = new Transform(nameof(Transform), this);

            NoAspectRatio = false;
            TransformModifiers = new ObservableCollection<ITransformModifier>();

            AddTransformModifierCommand = new RelayCommand(p => AddTransformModifier((TransformModifierNames)p));
            RemoveTransformModifierCommand = new RelayCommand(p => RemoveTransformModifier(p as ITransformModifier));
        }

        public ICommand AddTransformModifierCommand { get; set; }
        public ICommand RemoveTransformModifierCommand { get; set; }

        private TransformModifierFactory Factory { get; set; }

        public override void Receive(IMessage message)
        {
            var mess = message as Message;

            switch (mess.Command)
            {
                case MessageCommand.ADD_TRANSFORMMODIFIER:
                    {
                        var model = message.Obj as ITransformModifierModel;
                        TransformModifierNames name = (TransformModifierNames)mess.CommandParameter;
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
            Message message = new Message(MessageCommand.ADD_TRANSFORMMODIFIER, this.GetAddress(), transformModifier.GetModel(), transformModifierNames);
            this.Send(message);
        }

        public void RemoveTransformModifier(ITransformModifier transformModifier)
        {
            transformModifier.Dispose();
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
    }
}