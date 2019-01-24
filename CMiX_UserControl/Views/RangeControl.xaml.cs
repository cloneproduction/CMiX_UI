using System.Windows;
using System.Windows.Controls;

namespace CMiX.Views
{
    public partial class RangeControl : UserControl
    {
        public RangeControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty CaptionProperty =
        DependencyProperty.Register("Caption", typeof(string), typeof(RangeControl), new PropertyMetadata(""));
        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }
    }
}
