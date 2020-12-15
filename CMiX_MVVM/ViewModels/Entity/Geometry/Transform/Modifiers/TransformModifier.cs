using System;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class TransformModifier : Sender
    {
        public TransformModifier(string name, Sender parentSender, Transform transform, MasterBeat beat) : base(name, parentSender)
        {
            TranslateModifier = new TranslateModifier(nameof(TranslateModifier), this, transform.Translate, beat);
            ScaleModifier = new ScaleModifier(nameof(ScaleModifier), this, transform.Scale, beat);
            RotationModifier = new RotationModifier(nameof(RotationModifier), this, transform.Rotation, beat);
        }

        public override void Receive(Message message)
        {
            throw new NotImplementedException();
        }

        public TranslateModifier TranslateModifier { get; set; }
        public ScaleModifier ScaleModifier { get; set; }
        public RotationModifier RotationModifier { get; set; }
    }
}