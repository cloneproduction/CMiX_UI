using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Collections.Specialized;
using System.ComponentModel;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;
using GongSolutions.Wpf.DragDrop;

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
            DeleteItemCommand = new RelayCommand(p => DeleteItem(p));
            FilePaths = new ObservableCollection<FileNameItem>();
            FilePaths.CollectionChanged += ContentCollectionChanged;
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
            FilePaths.Clear();
        }

        private void ClearUnselected()
        {
            for (int i = FilePaths.Count - 1; i >= 0; i--)
            {
                if (!FilePaths[i].FileIsSelected)
                    FilePaths.Remove(FilePaths[i]);
            }
        }

        private void ClearSelected()
        {
            for (int i = FilePaths.Count - 1; i >= 0; i--)
            {
                if (FilePaths[i].FileIsSelected)
                    FilePaths.Remove(FilePaths[i]);
            }
        }

        private void DeleteItem(object filenameitem)
        {
            FileNameItem fni = filenameitem as FileNameItem;
            FilePaths.Remove(fni);
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

        #region COLLECTIONCHANGED
        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (FileNameItem item in e.OldItems)
                {
                    //Removed items
                    item.PropertyChanged -= EntityViewModelPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (FileNameItem item in e.NewItems)
                {
                    //Added items
                    item.PropertyChanged += EntityViewModelPropertyChanged;
                }
            }

            List<string> filename = new List<string>();
            foreach (FileNameItem lb in FilePaths)
            {
                if (lb.FileIsSelected == true)
                {
                    filename.Add(lb.FileName);
                }
            }
            Messenger.SendMessage(MessageAddress + nameof(FilePaths), filename.ToArray());
        }

        public void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            List<string> filename = new List<string>();
            foreach (FileNameItem lb in FilePaths)
            {
                if (lb.FileIsSelected == true)
                {
                    filename.Add(lb.FileName);
                }
            }
            Messenger.SendMessage(MessageAddress + nameof(FilePaths), filename.ToArray());
        }
        #endregion

        #region COPY/PASTE
        public void Copy(FileSelectorDTO fileselectordto)
        {
            fileselectordto.FilePaths = FilePaths.ToList();
        }

        public void Paste(FileSelectorDTO fileselectordto)
        {
            Messenger.SendEnabled = false;
            FilePaths = new ObservableCollection<FileNameItem>(fileselectordto.FilePaths as List<FileNameItem>);
            Messenger.SendEnabled = true;
        }
        #endregion
    }
}