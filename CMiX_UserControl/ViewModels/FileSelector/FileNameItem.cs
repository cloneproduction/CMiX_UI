using System.Collections.ObjectModel;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.Services;
using CMiX.MVVM;

namespace CMiX.Studio.ViewModels 
{
    public class FileNameItem : ViewModel, ICopyPasteModel, ISendable
    {
        public FileNameItem(string folderpath, string messageAddress, Messenger messenger)
        {
            MessageAddress = messageAddress + "Selected";
            Messenger = messenger;
            FolderPath = folderpath;

        }

        private string _folderpath;
        public string FolderPath
        {
            get => _folderpath;
            set => SetAndNotify(ref _folderpath, value);

        }

        private string _filename;
        public string FileName
        {
            get => _filename;
            set => SetAndNotify(ref _filename, value);
        }

        private bool _fileisselected;
        public bool FileIsSelected
        {
            get => _fileisselected;
            set
            {
                SetAndNotify(ref _fileisselected, value);
                if (FileIsSelected)
                {
                    if(!string.IsNullOrEmpty(this.FileName) && !string.IsNullOrEmpty(this.FolderPath))
                    {
                        string fn = Utils.GetRelativePath(FolderPath, this.FileName);
                        //SendMessages(MessageAddress, fn);
                    }
                    //else
                        //SendMessages(MessageAddress, FileName);
                }
            }
        }

        public string MessageAddress { get ; set ; }
        public Messenger Messenger { get; set; }

        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress + "Selected";
        }

        #region COPY/PASTE
        public void CopyModel(IModel model)
        {
            FileNameItemModel filenameitemmodel = model as FileNameItemModel;
            filenameitemmodel.MessageAddress = MessageAddress;
            filenameitemmodel.FileIsSelected = FileIsSelected;
            filenameitemmodel.FileName = FileName;
        }

        public void PasteModel(IModel model)
        {
            Messenger.Disable();

            FileNameItemModel filenameitemmodel = model as FileNameItemModel;
            MessageAddress = filenameitemmodel.MessageAddress;
            FileIsSelected = filenameitemmodel.FileIsSelected;
            FileName = filenameitemmodel.FileName;

            Messenger.Enable();
        }
        #endregion
    }
}