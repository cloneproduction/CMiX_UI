using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CMiX.MVVM.Controls
{
    public class DummyDO : DependencyObject
    {
        public DummyDO()
        {

        }

        public static readonly DependencyProperty NameProperty =
        DependencyProperty.Register("Name", typeof(string), typeof(DummyDO), new FrameworkPropertyMetadata(string.Empty));
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly DependencyProperty AnimationPositionProperty =
        DependencyProperty.Register("AnimationPosition", typeof(double), typeof(DummyDO), new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(OnAnimationChange)));
        public double AnimationPosition
        {
            get { return (double)GetValue(AnimationPositionProperty); }
            set { SetValue(AnimationPositionProperty, value); }
        }

        private static void OnAnimationChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pouet = (DummyDO)d;
            //Console.WriteLine("________");
            Console.WriteLine(pouet.Name + " " +(double)e.NewValue);
            //Console.WriteLine("++++++++");
        }
    }
}
