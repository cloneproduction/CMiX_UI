using System.Windows;

namespace CMiX.MVVM.Controls
{
    public class AnimatedDouble : DependencyObject
    {
        public AnimatedDouble()
        {

        }

        public static readonly DependencyProperty AnimationPositionProperty =
        DependencyProperty.Register("AnimationPosition", typeof(double), typeof(AnimatedDouble), new FrameworkPropertyMetadata(0.0));

        public double AnimationPosition
        {
            get { return (double)GetValue(AnimationPositionProperty); }
            set { SetValue(AnimationPositionProperty, value); }
        }
    }
}
