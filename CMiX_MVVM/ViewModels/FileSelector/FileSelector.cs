using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using CMiX.MVVM.Models;

using GongSolutions.Wpf.DragDrop;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class FileSelector : ViewModel, ISendable, IUndoable, IDropTarget, IDragSource
    {
        #region CONSTRUCTORS
        public FileSelector(string messageaddress, string selectionmode, List<string> filemask, Sender sender, Mementor mementor) 
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(FileSelector));
            Sender = sender;

            SelectionMode = selectionmode;
            FileMask = filemask;
            FilePaths = new ObservableCollection<FileNameItem>();

            ClearSelectedCommand = new RelayCommand(p => ClearSelected());
            ClearUnselectedCommand = new RelayCommand(p => ClearUnselected());
            ClearAllCommand = new RelayCommand(p => ClearAll());
            DeleteItemCommand = new RelayCommand(p => DeleteItem(p));
            ResetCommand = new RelayCommand(p => Reset());
        }
        #endregion

        #region PROPERTIES
        public ObservableCollection<FileNameItem> FilePaths { get; set; }

        public List<string> FileMask { get; set; }
        public string SelectionMode { get; set; }

        public ICommand ClearSelectedCommand { get; }
        public ICommand ClearUnselectedCommand { get; }
        public ICommand ClearAllCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand ResetCommand { get; }

        private FileNameItem selectedfilenameitem;
        public FileNameItem SelectedFileNameItem
        {
            get { return selectedfilenameitem; }
            set
            {
                SetAndNotify(ref selectedfilenameitem, value);
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(SelectedFileNameItem));
            }
        }

        private string _folderpath;
        public string FolderPath
        {
            get => _folderpath;
            set => SetAndNotify(ref _folderpath, value);
        }
        public string MessageAddress { get; set; }
        public Sender Sender { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
            foreach (var item in FilePaths)
            {
                item.UpdateMessageAddress(messageaddress);
            }
        }

        private void ClearAll()
        {
            Mementor.PropertyChange(this, "FilePaths");
            FilePaths.Clear();
        }

        private void ClearUnselected()
        {
            Mementor.Batch(() =>
            {
                for (int i = FilePaths.Count - 1; i >= 0; i--)
                {
                    if (!FilePaths[i].FileIsSelected)
                    {
                        Mementor.ElementRemove(FilePaths, FilePaths[i]);
                        FilePaths.Remove(FilePaths[i]);
                    }
                }
            });
        }

        private void ClearSelected()
        {
            Mementor.Batch(() =>
            {
                for (int i = FilePaths.Count - 1; i >= 0; i--)
                {
                    if (FilePaths[i].FileIsSelected)
                    {
                        Mementor.ElementRemove(FilePaths, FilePaths[i]);
                        FilePaths.Remove(FilePaths[i]);
                    }
                }
            });
        }

        private void DeleteItem(object filenameitem)
        {
            FileNameItem fni = filenameitem as FileNameItem;
            Mementor.ElementRemove(FilePaths, fni);
            FilePaths.Remove(fni);
        }
        #endregion

        #region DRAG/DROP

        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;

            if (dropInfo.Data is IDataObject dataObject && dataObject.GetDataPresent(DataFormats.FileDrop))
            {
                dropInfo.Effects = DragDropEffects.Copy;
            }

            if (dropInfo.Data.GetType() == typeof(FileNameItem))
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                dropInfo.Effects = DragDropEffects.Copy;
            }

            if (Mementor.IsInBatch)
            {
                Mementor.EndBatch();
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is DataObject dataObject)
            {
                if (dataObject.ContainsFileDropList())
                {
                    Mementor.BeginBatch();
                    var filedrop = dataObject.GetFileDropList();
                    foreach (string str in filedrop)
                    {
                        foreach (string fm in FileMask)
                        {
                            if (System.IO.Path.GetExtension(str).ToUpperInvariant() == fm)
                            {
                                FileNameItem lbfn = new FileNameItem(MessageAddress, Sender)
                                {
                                    FileIsSelected = false,
                                    //if(FolderPath != null)
                                    //{
                                    //    lbfn.FileName = Utils.GetRelativePath(FolderPath, str);
                                    //}
                                    //else
                                    //{

                                    //}
                                    FileName = str
                                };
                                FilePaths.Add(lbfn);
                                Mementor.ElementAdd(FilePaths, lbfn);
                            }
                        }
                    }
                    Mementor.EndBatch();
                }
            }

            if (dropInfo.DragInfo != null)
            {

                if (dropInfo.DragInfo.VisualSource != dropInfo.VisualTarget && dropInfo.Data.GetType() == typeof(FileNameItem))
                {
                    FileNameItem filenameitem = dropInfo.Data as FileNameItem;
                    foreach (string fm in FileMask)
                    {
                        if (System.IO.Path.GetExtension(filenameitem.FileName).ToUpperInvariant() == fm)
                        {
                            FileNameItem newfilenameitem = filenameitem.Clone() as FileNameItem;
                            newfilenameitem.FileIsSelected = true;
                            newfilenameitem.UpdateMessageAddress(MessageAddress);
                            SelectedFileNameItem = newfilenameitem;
                            FilePaths.Insert(dropInfo.InsertIndex, newfilenameitem);
                            Mementor.ElementAdd(FilePaths, newfilenameitem);
                        }
                    }
                }
            }
        }

        public void StartDrag(IDragInfo dragInfo)
        {
            FileNameItem filenameitem = (FileNameItem)dragInfo.SourceItem;
            dragInfo.Effects = DragDropEffects.Copy | DragDropEffects.Move;
            dragInfo.Data = filenameitem;
        }

        public bool CanStartDrag(IDragInfo dragInfo)
        {
            if (dragInfo.SourceItem.GetType() == typeof(FileNameItem))
            {
                return true;
            }
            return false;
        }

        public void Dropped(IDropInfo dropInfo)
        {
            if (dropInfo.DragInfo != null)
            {
                if (dropInfo.DragInfo.VisualSource == dropInfo.VisualTarget && dropInfo.Data.GetType() == typeof(FileNameItem))
                {
                    FileNameItem filenameitem = dropInfo.Data as FileNameItem;
                    FileNameItem newfilenameitem = filenameitem.Clone() as FileNameItem;

                    newfilenameitem.UpdateMessageAddress(MessageAddress);
                    FilePaths.Insert(dropInfo.InsertIndex, newfilenameitem);
                    FilePaths.Remove(filenameitem);
                    newfilenameitem.FileIsSelected = true;
                    SelectedFileNameItem = newfilenameitem;
                    Mementor.ElementAdd(FilePaths, newfilenameitem);
                }
            }
        }

        public void DragDropOperationFinished(DragDropEffects operationResult, IDragInfo dragInfo)
        {
            //throw new NotImplementedException();
        }

        public void DragCancelled()
        {
            //throw new NotImplementedException();
        }

        public bool TryCatchOccurredException(Exception exception)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region COPY/PASTE/RESET

        public void Reset()
        {
            Sender.Enable();

            Mementor.PropertyChange(this, "FilePaths");
            FilePaths.Clear();

            Sender.Enable();
        }

        public void Copy(FileSelectorModel fileselectormodel)
        {
            fileselectormodel.MessageAddress = MessageAddress;
            List<FileNameItemModel> FileNameItemModelList = new List<FileNameItemModel>();
            foreach (var item in FilePaths)
            {
                var filenameitemmodel = new FileNameItemModel();
                
                item.Copy(filenameitemmodel);
                
                FileNameItemModelList.Add(filenameitemmodel);
            }
            fileselectormodel.FilePaths = FileNameItemModelList;
        }

        public void Paste(FileSelectorModel fileselectormodel)
        {
            Sender.Enable();

            MessageAddress = fileselectormodel.MessageAddress;
            FilePaths.Clear();

            foreach (var item in fileselectormodel.FilePaths)
            {
                FileNameItem filenameitem = new FileNameItem(MessageAddress, Sender);
                filenameitem.Paste(item);
                if (filenameitem.FileIsSelected)
                    this.SelectedFileNameItem = filenameitem;
                //filenameitem.UpdateMessageAddress(MessageAddress);
                FilePaths.Add(filenameitem);
            }

            Sender.Enable();
        }
        #endregion
    }
}
 