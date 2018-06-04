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
            if (color < 0)
            {
                throw new ArgumentException("Color must not be negative.");
            }

            Color = color;
            Modifier = modifier;
        }

        private double _color;
        public double Color
        {
            get => _color;
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                SetAndNotify(ref _color, value);
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
