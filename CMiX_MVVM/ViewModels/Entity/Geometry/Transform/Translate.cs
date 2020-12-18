using System.Windows.Media.Media3D;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using CMiX.MVVM.ViewModels.Observer;

namespace CMiX.MVVM.ViewModels
{
    public class Translate : Sender, IObserver
    {
        public Translate(string name, IColleague parentSender) : base(name, parentSender)
        {
            X = new Slider(nameof(X), this);
            Y = new Slider(nameof(Y), this);
            Z = new Slider(nameof(Z), this);

            XYZ = new Vector3D[Count];
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }

        public int Count { get; set; }

        public Vector3D[] XYZ {get; set;}

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as TranslateModel);
        }


        public void Update(int count)
        {
            //XYZ = new Vector3D[count];
        }
    }
}