using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.Observer;

namespace CMiX.MVVM.ViewModels
{
    public class Transform : Sender, IObserver
    {
        public Transform(string name, IColleague parentSender) : base (name, parentSender)
        {
            Translate = new Translate(nameof(Translate), this);
            Scale = new Scale(nameof(Scale), this);
            Rotation = new Rotation(nameof(Rotation), this);
            Is3D = false;
            Count = 0;
        }

        public Translate Translate { get; set; }
        public Scale Scale { get; set; }
        public Rotation Rotation { get; set; }

        
        private int _count;
        public int Count
        {
            get { return _count; }
            set 
            { 
                _count = value;
                Translate.Count = value;
            }
        }


        private bool _is3D;
        public bool Is3D
        {
            get => _is3D;
            set
            {
                SetAndNotify(ref _is3D, value);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.Address, this.GetModel()));
            }
        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as TransformModel);
        }

        public void Update(int count)
        {
            this.Count = count;
        }
    }
}