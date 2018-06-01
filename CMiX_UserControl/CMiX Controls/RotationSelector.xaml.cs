using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CMiX
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class RotationSelector : UserControl, INotifyPropertyChanged
    {
        public RotationSelector()
        {
            InitializeComponent();
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;


        public event EventHandler SelectionChanged;
        private void OnSelectionChanged()
        {
            //Null check makes sure the main page is attached to the event
            if (this.SelectionChanged != null)
                this.SelectionChanged(this, new EventArgs());
        }

        public static readonly DependencyProperty RotationProperty =
        DependencyProperty.Register("Rotation", typeof(ObservableCollection<string>), typeof(RotationSelector), new PropertyMetadata(new ObservableCollection<string> { "STD_CTR", "True", "True", "True" }));
        [Bindable(true)]
        public ObservableCollection<string> Rotation
        {
            get { return (ObservableCollection<string>)this.GetValue(RotationProperty); }
            set { this.SetValue(RotationProperty, value); }
        }

        /*public static readonly DependencyProperty RotationAxisProperty =
        DependencyProperty.Register("RotationAxis", typeof(List<string>), typeof(RotationSelector), new PropertyMetadata(new List<string> { "True", "True", "True" }));
        [Bindable(true)]
        public List<string> RotationAxis
        {
            get { return (List<string>)this.GetValue(RotationAxisProperty); }
            set { this.SetValue(RotationAxisProperty, value); }
        }*/

        public static readonly DependencyProperty RotationAxisProperty =
        DependencyProperty.Register("RotationAxis", typeof(ObservableCollection<string>), typeof(RotationSelector), new PropertyMetadata(new ObservableCollection<string> { "True", "True", "True" }));
        [Bindable(true)]
        public ObservableCollection<string> RotationAxis
        {
        get { return (ObservableCollection<string>)this.GetValue(RotationAxisProperty); }
        set { this.SetValue(RotationAxisProperty, value); }
        }


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if(rb.IsChecked == true)
            {
                this.Rotation[0] = rb.Name;
            }
            OnSelectionChanged();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            OnSelectionChanged();
        }
    }
}
