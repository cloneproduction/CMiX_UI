using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CMiX
{
    public partial class LayerButton : UserControl
    {
        private ObservableCollection<RadioButton> _LayerBtn = new ObservableCollection<RadioButton>();
        public ObservableCollection<RadioButton> LayerBtn
        {
            get { return _LayerBtn; }
            set { _LayerBtn = value; }
        }

        public LayerButton()
        {
            InitializeComponent();
            string groupname = "pouet";
            RadioButton rb = new RadioButton();
            RadioButton rb1 = new RadioButton();

            LayerBtn.Add(rb);
            LayerBtn.Add(rb1);
        }

        private Point _dragStartPoint;

        private void ListBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.GetPosition(null);
            Vector diff = _dragStartPoint - point;
            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                var lb = sender as ListBox;
                var lbi = FindVisualParent<ListBoxItem>(((DependencyObject)e.OriginalSource));
                if (lbi != null)
                {
                    DragDrop.DoDragDrop(lbi, lbi.DataContext, DragDropEffects.Move);
                }
            }
        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            if (sender is ListBoxItem)
            {
                var source = e.Data.GetData(typeof(RadioButton)) as RadioButton;
                var target = ((ListBoxItem)(sender)).DataContext as RadioButton;

                int sourceIndex = BtnList.Items.IndexOf(source);
                int targetIndex = BtnList.Items.IndexOf(target);

                Move(source, sourceIndex, targetIndex);
            }
        }

        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(null);
        }

        private void Move(RadioButton source, int sourceIndex, int targetIndex)
        {
            if (sourceIndex < targetIndex)
            {
                var items = BtnList.ItemsSource as IList<RadioButton>;
                if (items != null)
                {
                    items.Insert(targetIndex + 1, source);
                    items.RemoveAt(sourceIndex);
                }
            }
            else
            {
                var items = BtnList.ItemsSource as IList<RadioButton>;
                if (items != null)
                {
                    int removeIndex = sourceIndex + 1;
                    if (items.Count + 1 > removeIndex)
                    {
                        items.Insert(targetIndex, source);
                        items.RemoveAt(removeIndex);
                    }
                }
            }
        }
        private P FindVisualParent<P>(DependencyObject child) where P : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;

            P parent = parentObject as P;
            if (parent != null)
                return parent;

            return FindVisualParent<P>(parentObject);
        }
    }
}
