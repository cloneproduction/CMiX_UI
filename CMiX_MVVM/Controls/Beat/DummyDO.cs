using System.Windows;

namespace CMiX.MVVM.Controls
{
    public class DummyDO : DependencyObject
    {
        public DummyDO()
        {

        }

        public static readonly DependencyProperty AnimationPositionProperty =
        DependencyProperty.Register("AnimationPosition", typeof(double), typeof(DummyDO), new FrameworkPropertyMetadata(0.0));

        public double AnimationPosition
        {
            get { return (double)GetValue(AnimationPositionProperty); }
            set { SetValue(AnimationPositionProperty, value); }
        }
    }
}
