using System;

namespace CMiX.ViewModels
{
    public class HSVPoint : ViewModel
    {
        public HSVPoint()
            : this(color: 0.0, modifier: default)
        { }

        public HSVPoint(double color, ColorationModifier modifier)
        {
            AssertNotNegative(() => color);

            Color = color;
            Modifier = modifier;
        }

        private double _color;
        public double Color
        {
            get => _color;
            set
            {
                SetAndNotify(ref _color, CoerceNotNegative(value));
            }
        }

        private ColorationModifier _modifier;
        public ColorationModifier Modifier
        {
            get => _modifier;
            set => SetAndNotify(ref _modifier, value);
        }
    }
}
