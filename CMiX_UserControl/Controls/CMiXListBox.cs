using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CMiX
{
    public class CMiXListBox : ListBox
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CMiXListBoxItem();
        }
    }

    public class CMiXListBoxItem : ListBoxItem
    {
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (IsSelected)
                e.Handled = true;
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (IsSelected)
                base.OnMouseLeftButtonDown(e);

            if (!IsSelected)
            {
                IsSelected = true;
            }
        }
    }
    

    [Serializable]
    public class ListBoxFileName : INotifyPropertyChanged, ICloneable
    {
        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        private string FileNameValue;
        public string FileName
        {
            get { return FileNameValue; }

            set
            {
                if (value != FileNameValue)
                {
                    FileNameValue = value;
                    NotifyPropertyChanged("FileName");
                }
            }
        }

        private bool FileIsSelectedValue;
        public bool FileIsSelected
        {
            get { return FileIsSelectedValue; }

            set
            {
                if (value != FileIsSelectedValue)
                {
                    FileIsSelectedValue = value;
                    NotifyPropertyChanged("FileIsSelected");
                }
            }
        }
    }
}
