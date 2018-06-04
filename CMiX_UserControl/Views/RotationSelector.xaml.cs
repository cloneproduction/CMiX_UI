using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CMiX
{
    public partial class RotationSelector : UserControl, INotifyPropertyChanged
    {

        Messenger message = new Messenger();

        public RotationSelector()
        {
            InitializeComponent();
            //this.DataContext = rotationselectorviewmodel;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler SelectionChanged;
        private void OnSelectionChanged()
        {
            if (SelectionChanged != null)
                SelectionChanged(this, new EventArgs());
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if(rb.IsChecked == true)
            {
                //rotationselectorviewmodel.Rotation = rb.Name;
                string name = Utils.FindParent<ChannelControls>(this).Name;
                message.SendOSC(name + "/" + Name, rb.Name);
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            OnSelectionChanged();
        }

        private void PopupRotation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                PopupRotation.IsOpen = false;
            }
        }
    }
}
