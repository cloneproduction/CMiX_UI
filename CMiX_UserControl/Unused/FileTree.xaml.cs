using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CMiX
{
    public partial class CustomTree : UserControl
    {
        public CustomTree()
        {
            InitializeComponent();
        }
        TreeView TreeV = new TreeView();

        private void FileTree_PreviewDrop(object sender, DragEventArgs e)
        {
            foreach (var s in (string[])e.Data.GetData(DataFormats.FileDrop, false))
            {
                if (Directory.Exists(s))
                {
                    Populate(s, s, CMiXTreeView, null, false);
                }
                else
                {
                    TreeViewItem _driitem = new TreeViewItem();
                    _driitem.Tag = s;
                    _driitem.Header = s;
                    CMiXTreeView.Items.Add(_driitem);
                }
            }
        }

        private void Populate(string header, string tag, TreeView _root, TreeViewItem _child, bool isfile)
        {
            TreeViewItem _driitem = new TreeViewItem();
            _driitem.Tag = tag;
            _driitem.Header = header;
            _driitem.Expanded += new RoutedEventHandler(_driitem_Expanded);

            if (!isfile)
            {
                _driitem.Items.Add(new TreeViewItem());
            }

            if (_root != null)
            {
                _root.Items.Add(_driitem);
            }
            else
            {
                _child.Items.Add(_driitem);
            }
        }

        void _driitem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem _item = (TreeViewItem)sender;
            if (_item.Items.Count == 1 && ((TreeViewItem)_item.Items[0]).Header == null)
            {
                _item.Items.Clear();
                foreach (string dir in Directory.GetDirectories(_item.Tag.ToString()))
                {
                    DirectoryInfo _dirinfo = new DirectoryInfo(dir);
                    Populate(_dirinfo.Name, _dirinfo.FullName, null, _item, false);
                }

                foreach (string dir in Directory.GetFiles(_item.Tag.ToString()))
                {
                    FileInfo _dirinfo = new FileInfo(dir);
                    Populate(_dirinfo.Name, _dirinfo.FullName, null, _item, true);
                }
            }
        }



        private bool _isMouseDown;
        private Point _dragStartPoint;
        private TreeViewItem _dragged;

        private void TreeViewTest_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            
            if (_isMouseDown == true && IsDragGesture(e.GetPosition(e.Source as FrameworkElement)))
            {
                if (_dragged == null)
                    return;
                if (e.LeftButton == MouseButtonState.Released)
                {
                    _dragged = null;
                    return;
                }

                object temp = CMiXTreeView.SelectedItem;
                DataObject data = null;
                data = new DataObject("test", temp);

                if(data != null)
                {
                    DragDropEffects de = DragDrop.DoDragDrop(CMiXTreeView, data, DragDropEffects.All);
                }

            }
        }

        private bool IsDragGesture(Point point)
        {
            var horizontalMove = Math.Abs(point.X - _dragStartPoint.X);
            var verticalMove = Math.Abs(point.Y - _dragStartPoint.Y);
            var hGesture = horizontalMove > SystemParameters.MinimumHorizontalDragDistance;
            var vGesture = verticalMove > SystemParameters.MinimumVerticalDragDistance;
            return (hGesture | vGesture);
        }

        private void TreeViewTest_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            UIElement element = CMiXTreeView.InputHitTest(e.GetPosition(CMiXTreeView)) as UIElement;
           
            while(element != null)
            {
                if(element is TreeViewItem)
                {
                    TreeViewItem item = (TreeViewItem)element;
                    FileAttributes attr = File.GetAttributes(item.Tag.ToString());
                    if(attr.HasFlag(FileAttributes.Directory) != true)
                    {
                        _dragged = item;
                    }
                }
                element = VisualTreeHelper.GetParent(element) as UIElement;
            }

            if(e.ClickCount != 1)
            {
                return;
            }
            _dragStartPoint = e.GetPosition(e.Source as FrameworkElement);
            _isMouseDown = true;
        }
    }
}