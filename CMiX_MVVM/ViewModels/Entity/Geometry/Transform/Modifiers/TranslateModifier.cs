using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Observer;

namespace CMiX.MVVM.ViewModels
{
    public class TranslateModifier : ViewModel, ITransformModifier, IModifier, IObserver
    {
        public TranslateModifier(string name, Sender parentSender, MasterBeat beat) //: base(name, parentSender)
        {
            Count = 1;

            ModifierType = ModifierType.GROUP;

            TranslateXYZ = new Vector3D[1] { new Vector3D(0.0, 0.0, 0.0) };
            ScaleXYZ = new Vector3D[1] { new Vector3D(1.0, 1.0, 1.0) };
            RotationXYZ = new Vector3D[1] { new Vector3D(0.0, 0.0, 0.0) };

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
            if (ModifierType == ModifierType.OBJECT)
            {
                for (int i = 0; i < objectCount; i++)
                {
                    var doubleToAnimate = TranslateXYZ.Select(x => TranslateXYZ[i].X).ToArray();
                    X.AnimateOnGameLoop();
                }
            }
            else if (ModifierType == ModifierType.GROUP)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    var doubleToAnimate = TranslateXYZ.Select(x => TranslateXYZ[i].X).ToArray();
                    X.AnimateOnGameLoop();
                }
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


        public double[] TranslateX { get; set; }

        public Vector3D[] TranslateXYZ { get; set; }
        public Vector3D[] ScaleXYZ { get; set; }
        public Vector3D[] RotationXYZ { get; set; }
    }
}