using System.Windows.Media.Media3D;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Translate : Sender, IParameter
    {
        public Translate(string name, IColleague parentSender) : base(name, parentSender)
        {
            X = new Slider(nameof(X), this);
            Y = new Slider(nameof(Y), this);
            Z = new Slider(nameof(Z), this);

            Values = new double[3] { X.Amount, Y.Amount, Z.Amount };
            XYZ = new Vector3D[0];
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }
        public double[] Values { get; set; }
        public int Count { get; set; }

        public Vector3D[] XYZ { get; set; }


        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as TranslateModel);
        }

        public Vector3D[] GetXParameters()
        {
            XYZ = new Vector3D[Count];
            for (int i = 0; i < Count; i++)
            {
                XYZ[i] = new Vector3D(X.Amount, Y.Amount, Z.Amount); 
            }
            return XYZ;
        }
    }
}