using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CMiX
{
    public partial class CMiXSlider : UserControl, INotifyPropertyChanged
    {
        public CMiXSlider()
        {
            InitializeComponent();
            this.DataContext = this;

            CMiX_Slider.ApplyTemplate();

            Thumb thumb0 = (CMiX_Slider.Template.FindName("PART_Track", CMiX_Slider) as Track).Thumb;
            thumb0.MouseEnter += new MouseEventHandler(thumb_MouseEnter);
        }

        public static readonly DependencyProperty SliderValueProperty =
        DependencyProperty.Register("SliderValue", typeof(double), typeof(CMiXSlider), new PropertyMetadata(0.5));
        public double SliderValue
        {
            get { return (double)this.GetValue(SliderValueProperty); }
            set { this.SetValue(SliderValueProperty, value); }
        }

        /*private double slidervalue;
        public double SliderValue 
        { 
            get { return slidervalue; }
            set { 
                    slidervalue = value; 
                    OnPropertyChanged(new PropertyChangedEventArgs("SliderValue")); 
                } 
        } */

        public static readonly DependencyProperty SliderOrientationProperty =
        DependencyProperty.Register("SliderOrientation", typeof(string), typeof(CMiXSlider), new PropertyMetadata("Horizontal"));
        public string SliderOrientation
        {
            get { return (string)this.GetValue(SliderOrientationProperty); }
            set { this.SetValue(SliderOrientationProperty, value); }
        }

        private void thumb_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.MouseDevice.Captured == null)
            {
                MouseButtonEventArgs args = new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Left);
                args.RoutedEvent = MouseLeftButtonDownEvent;
                (sender as Thumb).RaiseEvent(args);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
