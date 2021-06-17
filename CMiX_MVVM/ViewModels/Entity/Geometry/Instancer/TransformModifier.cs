using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Beat;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CMiX.MVVM.ViewModels
{
    public class TransformModifier : Control
    {
        public TransformModifier(MasterBeat beat)
        {
            //this.ID = instancerModel.ID;
            TransformModifiers = new ObservableCollection<ITransformModifier>();
            Factory = new TransformModifierFactory(beat);
            CreateTransformModifierCommand = new RelayCommand(p => CreateTransformModifier((TransformModifierNames)p));
            RemoveTransformModifierCommand = new RelayCommand(p => RemoveTransformModifier(p as ITransformModifier));
        }


        public ICommand CreateTransformModifierCommand { get; set; }
        public ICommand AddTransformModifierCommand { get; set; }
        public ICommand RemoveTransformModifierCommand { get; set; }
        public TransformModifierFactory Factory { get; set; }


        private ObservableCollection<ITransformModifier> _transformModifiers;
        public ObservableCollection<ITransformModifier> TransformModifiers
        {
            get => _transformModifiers;
            set => SetAndNotify(ref _transformModifiers, value);
        }


        public void CreateTransformModifier(TransformModifierNames transformModifierNames)
        {
            ITransformModifier transformModifier = Factory.CreateTransformModifier(transformModifierNames);
            transformModifier.SetCommunicator(Communicator);
            AddTransformModifier(transformModifier);
        }


        public void AddTransformModifier(ITransformModifier transformModifier)
        {
            TransformModifiers.Add(transformModifier);
            Communicator?.SendMessage(new MessageAddTransformModifier(transformModifier.GetModel() as ITransformModifierModel));
        }


        public void RemoveTransformModifier(ITransformModifier transformModifier)
        {
            this.TransformModifiers.Remove(transformModifier);
        }


        public override IModel GetModel()
        {
            throw new NotImplementedException();
        }

        public override void SetViewModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}
