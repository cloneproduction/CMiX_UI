using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMiX.ViewModels;
using CMiX.Services;
using GuiLabs.Undo;
using System.Collections.ObjectModel;
using CMiX.Controls;
using System.Windows.Input;
using GongSolutions.Wpf.DragDrop;
using System.Windows;

namespace CMiX.ViewModels
{
    public class FileSelector : ViewModel, IDropTarget
    {
        #region CONSTRUCTORS
        public FileSelector(string layername, OSCMessenger messenger, ActionManager actionmanager)
            : this
            (
                messageaddress: String.Format("{0}/", layername),
                messenger: messenger,
                actionmanager: actionmanager
            )
        { }

        public FileSelector
            (
                OSCMessenger messenger,
                string messageaddress,
                ActionManager actionmanager
            )
            : base(actionmanager, messenger)
        {
            MessageAddress = messageaddress;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            ClearSelectedCommand = new RelayCommand(p => ClearSelected());
            ClearUnselectedCommand = new RelayCommand(p => ClearUnselected());
            ClearAllCommand = new RelayCommand(p => ClearAll());
            DeleteItemCommand = new RelayCommand(p => DeleteItem());
            FilePaths = new ObservableCollection<FileNameItem>();
        }
        #endregion

        #region PROPERTIES
        [OSC]
        public ObservableCollection<FileNameItem> FilePaths { get; set; }

        public ICommand ClearSelectedCommand { get; }
        public ICommand ClearUnselectedCommand { get; }
        public ICommand ClearAllCommand { get; }
        public ICommand DeleteItemCommand { get; }
        #endregion

        #region METHODS
        private void ClearAll()
        {
            Console.WriteLine("ClearAll");
        }

        private void ClearUnselected()
        {
            Console.WriteLine("ClearUnselected");
        }

        private void ClearSelected()
        {
            Console.WriteLine("ClearSelected");
        }

        private void DeleteItem()
        {
            Console.WriteLine("DeleteItem!!!");
        }

        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;

            var dataObject = dropInfo.Data as IDataObject;
            // look for drag&drop new files
            if (dataObject != null && dataObject.GetDataPresent(DataFormats.FileDrop))
            {
                dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as DataObject;
            // look for drag&drop new files
            if (dataObject != null && dataObject.ContainsFileDropList())
            {
                var pouet = dataObject.GetFileDropList();
                foreach (string str in pouet)
                {
                    FileNameItem lbfn = new FileNameItem();
                    lbfn.FileName = str;
                    lbfn.FileIsSelected = false;
                    FilePaths.Add(lbfn);
                }
            }
        }
        #endregion
    }
}
