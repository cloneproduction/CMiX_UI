using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CMiX.Controls
{
    public partial class FileSelector : UserControl, INotifyPropertyChanged
    {
        public FileSelector()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #region Properties
        public static readonly DependencyProperty ItemsProperty =
        DependencyProperty.Register("Items", typeof(ObservableCollection<ListBoxFileName>), typeof(FileSelector));
        [Bindable(true)]
        public ObservableCollection<ListBoxFileName> Items
        {
            get { return (ObservableCollection<ListBoxFileName>)GetValue(ItemsProperty); }
            set { NotifyPropertyChanged("Items");  SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ModeSelectionProperty =
        DependencyProperty.Register("ModeSelection", typeof(string), typeof(FileSelector));
        [Bindable(true)]
        public string ModeSelection
        {
            get { return (string)GetValue(ModeSelectionProperty); }
            set { SetValue(ModeSelectionProperty, value); }
        }

        public static readonly DependencyProperty FileMaskProperty =
        DependencyProperty.Register("FileMask", typeof(List<string>), typeof(FileSelector));
        [Bindable(true)]
        public List<string> FileMask
        {
            get { return (List<string>)GetValue(FileMaskProperty); }
            set { SetValue(FileMaskProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(FileSelector));
        [Bindable(true)]
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        #endregion


        private void DeleteFileName_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = FileNameList.Items.IndexOf(btn.DataContext);
            Items.RemoveAt(index);
        }

        private ListBoxItem _dragged;
        private Point _dragStartPoint;
        private bool _isMouseDown;

        private bool IsDragGesture(Point point)
        {
            var horizontalMove = Math.Abs(point.X - _dragStartPoint.X);
            var verticalMove = Math.Abs(point.Y - _dragStartPoint.Y);
            var hGesture = horizontalMove > SystemParameters.MinimumHorizontalDragDistance;
            var vGesture = verticalMove > SystemParameters.MinimumVerticalDragDistance;
            return (hGesture | vGesture);
        }

        //bool _ClickOnItem = false;

        private void FileNameList_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_dragged != null)
            {
                return;
            }

            UIElement element = FileNameList.InputHitTest(e.GetPosition(FileNameList)) as UIElement;
            while (element != null)
            {
                if (element is ListBoxItem)
                {
                    _dragged = (ListBoxItem)element;
                    break;
                }
                element = VisualTreeHelper.GetParent(element) as UIElement;
            }

            if (e.ClickCount != 1)
            {
                return;
            }
            _dragStartPoint = e.GetPosition(e.Source as FrameworkElement);

            _isMouseDown = true;
        }

        private void FileListBox_PreviewMouseMove(object sender, MouseEventArgs e)
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

                List<ListBoxFileName> _draggedList = new List<ListBoxFileName>();
                if (_dragged.IsSelected == true)
                {

                    for (var i = 0; i < FileNameList.SelectedItems.Count; i++)
                    {
                        _draggedList.Add(FileNameList.SelectedItems[i] as ListBoxFileName);
                    }

                    var dragData = new DataObject(typeof(List<ListBoxFileName>), _draggedList);
                    var dragSource = this;
                    dragData.SetData("DragSource", dragSource);
                    DragDrop.DoDragDrop(_dragged, dragData, DragDropEffects.All);
                }
                else
                {
                    ListBoxFileName lb = _dragged.Content as ListBoxFileName;
                    _draggedList.Add(lb);
                    var dragData = new DataObject(typeof(List<ListBoxFileName>), _draggedList);
                    var dragSource = this;
                    dragData.SetData("DragSource", dragSource);
                    DragDrop.DoDragDrop(_dragged, dragData, DragDropEffects.All);
                }
            }
        }



        private void FileListBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                string[] droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];

                for (var i = 0; i < droppedFilePaths.Length; i++)
                {
                    //if (Path.GetExtension(droppedFilePaths[i]) == filemask)
                    //{
                    ListBoxFileName filename = new ListBoxFileName();
                    filename.FileName = droppedFilePaths[i];
                    filename.FileIsSelected = false;
                    this.Items.Add(filename);
                    // }
                }
            }

            DependencyObject child = e.OriginalSource as DependencyObject;

            var dataObj = e.Data as DataObject;
            var dragSource = e.Data.GetData("DragSource") as FileSelector;

            if (dragSource != null)
            {
                string dragFileSelectorSourceName = dragSource.Name.ToString();
                string dragFileSelectorTargetName = Utils.FindParent<FileSelector>(child).Name.ToString();

                string dragChannelControlsSourceName = Utils.FindParent<Views.LayerControls>(dragSource).Name.ToString();
                string dragChannelControlsTargetName = Utils.FindParent<Views.LayerControls>(child).Name.ToString();

                List<ListBoxFileName> FileNames = dataObj.GetData(typeof(List<ListBoxFileName>)) as List<ListBoxFileName>;

                if (dragChannelControlsSourceName != dragChannelControlsTargetName || dragFileSelectorSourceName != dragFileSelectorTargetName)
                {
                    foreach (var Files in FileNames)
                    {
                        ListBoxFileName CopiedFileName = Files.Clone() as ListBoxFileName;
                        CopiedFileName.FileIsSelected = false;
                        Items.Add(CopiedFileName);
                    }
                }
            }
        }


        private void ClearSelected_Click(object sender, RoutedEventArgs e)
        {
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                if (Items[i].FileIsSelected == true)
                {
                    Items.RemoveAt(i);
                }
            }
        }

        private void ClearUnselected_Click(object sender, RoutedEventArgs e)
        {
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                if (Items[i].FileIsSelected == false)
                {
                    Items.RemoveAt(i);
                }
            }
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            Items.Clear();
        }
    }
}