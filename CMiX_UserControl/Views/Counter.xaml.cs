using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace CMiX
{
    public partial class Counter : UserControl
    {
        CounterViewModel counterviewmodel = new CounterViewModel();


        public Counter()
        {
            InitializeComponent();
            this.DataContext = counterviewmodel;
        }
       
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            if (counterviewmodel.Count <= 10)
            {
                counterviewmodel.Count += 1;
            }
            string name = Utils.FindParent<ChannelControls>(this).Name;
            message.SendOSC(name + "/" + this.Name, counterviewmodel.Count.ToString());
        }

        private void Button_Sub(object sender, RoutedEventArgs e)
        {
            if (counterviewmodel.Count >= 1)
            {
                counterviewmodel.Count -= 1;
            }
            string name = Utils.FindParent<ChannelControls>(this).Name;
            message.SendOSC(name + "/" + this.Name, counterviewmodel.Count.ToString());
        }

        Messenger message = new Messenger();

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        // Use the DataObject.Pasting Handler 
        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        public event EventHandler CounterChanged;
        protected virtual void OnCounterChanged(EventArgs e)
        {
            var handler = CounterChanged;
            if (handler != null)
                handler(this, e);
        }

        private void CounterValue_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void CounterValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            //OnCounterChanged(e);
        }
    }
}
