using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CMiX
{
    public partial class FFTControls : UserControl
    {
        public FFTControls()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty FFTNamesProperty =
        DependencyProperty.Register("FFTNames", typeof(ObservableCollection<string>), typeof(FFTControls));
        [Bindable(true)]
        public string FFTNames
        {
            get { return (string)GetValue(FFTNamesProperty); }
            set { SetValue(FFTNamesProperty, value); }
        }

        public static readonly DependencyProperty SelectedFFTProperty =
        DependencyProperty.Register("SelectedFFT", typeof(string), typeof(FFTControls));
        [Bindable(true)]
        public string SelectedFFT
        {
            get { return (string)GetValue(SelectedFFTProperty); }
            set { SetValue(SelectedFFTProperty, value); }
        }

        public static readonly DependencyProperty FFTInfluenceProperty =
        DependencyProperty.Register("FFTInfluence", typeof(double), typeof(FFTControls), new PropertyMetadata(0.0));
        [Bindable(true)]
        public double FFTInfluence
        {
            get { return (double)GetValue(FFTInfluenceProperty); }
            set { SetValue(FFTInfluenceProperty, value); }
        }
    }
}
