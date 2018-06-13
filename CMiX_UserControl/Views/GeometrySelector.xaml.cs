using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CMiX.Views
{
    public partial class GeometrySelector : UserControl
    {
        public GeometrySelector()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly DependencyProperty ItemsProperty =
        DependencyProperty.Register("Items", typeof(ObservableCollection<ListBoxFileName>), typeof(FileSelector), new PropertyMetadata(new ObservableCollection<ListBoxFileName>()));
        [Bindable(true)]
        public ObservableCollection<ListBoxFileName> Items
        {
            get { return (ObservableCollection<ListBoxFileName>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        private void CMiXListBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                string[] droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];

                for (var i = 0; i < droppedFilePaths.Length; i++)
                {
                    ListBoxFileName filename = new ListBoxFileName {FileName = droppedFilePaths[i], FileIsSelected = false };
                    Items.Add(filename);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = GeometryList.Items.IndexOf(btn.DataContext);
            Items.RemoveAt(index);
        }
    }
}