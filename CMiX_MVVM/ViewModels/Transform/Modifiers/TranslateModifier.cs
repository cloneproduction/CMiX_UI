using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.Observer;
using System.Linq;
using System.Windows.Media.Media3D;

namespace CMiX.MVVM.ViewModels
{
    public class TranslateModifier : ViewModel, IModifier, IObserver
    {
        public TranslateModifier(string name, Module parentSender, MasterBeat beat)
        {
            Count = 1;

            ModifierType = ModifierType.GROUP;

            Location = new Vector3D[1] { new Vector3D(0.0, 0.0, 0.0) };
            Scale = new Vector3D[1] { new Vector3D(1.0, 1.0, 1.0) };
            Rotation = new Vector3D[1] { new Vector3D(0.0, 0.0, 0.0) };

            //X = new AnimParameter(nameof(X), this, new double[1] { 0.0 }, beat);
            //Y = new AnimParameter(nameof(Y), this, translate.Y.Amount, beat);
            //Z = new AnimParameter(nameof(Z), this, translate.Z.Amount, beat);
        }

        private ModifierType _modifierType;
        public ModifierType ModifierType
        {
            get => _modifierType;
            set => SetAndNotify(ref _modifierType, value);
        }


        private int _count;
        public int Count
        {
            get => _count;
            set => SetAndNotify(ref _count, value);
        }

        public void AnimateOnBeatTick()
        {

        }

        public void AnimateOnGameLoop(int objectCount)
        {
            int modifierCount = 0;
            if (ModifierType == ModifierType.OBJECT)
            {
                modifierCount = objectCount;
                if (objectCount != Location.Length)
                {
                    Location = new Vector3D[objectCount];
                    Scale = new Vector3D[objectCount];
                    Rotation = new Vector3D[objectCount];
                }
            }
            else if (ModifierType == ModifierType.GROUP)
            {
                modifierCount = Count;
            }

            var XToAnimate = Location.Select(x => x.X).ToArray();
            var YToAnimate = Location.Select(x => x.Y).ToArray();
            var ZToAnimate = Location.Select(x => x.Z).ToArray();

            X.AnimateOnGameLoop(XToAnimate);
            Y.AnimateOnGameLoop(YToAnimate);
            Z.AnimateOnGameLoop(ZToAnimate);

            for (int i = 0; i < modifierCount; i++)
            {
                Location[i].X = XToAnimate[i];
                Location[i].Y = YToAnimate[i];
                Location[i].Z = ZToAnimate[i];
            }
        }



        private void AnimateObjectsOnGameLoop()
        {

        }


        public void Update(int count)
        {
            this.Count = count;
            X.Parameters = new double[count];
        }

        public AnimParameter X { get; set; }
        public AnimParameter Y { get; set; }
        public AnimParameter Z { get; set; }

        double[] TranslateX { get; set; }
        double[] TranslateY { get; set; }
        double[] TranslateZ { get; set; }

        public Vector3D[] Location { get; set; }
        public Vector3D[] Scale { get; set; }
        public Vector3D[] Rotation { get; set; }
    }
}