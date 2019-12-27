using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Resources;

namespace CMiX.ViewModels 
{
    public class FileNameItem : SendableViewModel
    {
        public FileNameItem(string folderpath, string messageaddress, ObservableCollection<ServerValidation> serverValidations, Mementor mementor)
            : base (serverValidations, mementor)
        {
            MessageAddress = messageaddress + "Selected";
            FolderPath = folderpath;
            Mementor = mementor;
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

        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress + "Selected";
        }

        #region COPY/PASTE
        public void Copy(FileNameItemModel filenameitemmodel)
        {
            filenameitemmodel.MessageAddress = MessageAddress;
            filenameitemmodel.FileIsSelected = FileIsSelected;
            filenameitemmodel.FileName = FileName;
        }

        public void Paste(FileNameItemModel filenameitemmodel)
        {
            DisabledMessages();

            MessageAddress = filenameitemmodel.MessageAddress;
            FileIsSelected = filenameitemmodel.FileIsSelected;
            FileName = filenameitemmodel.FileName;

            EnabledMessages();
        }
        #endregion
    }
}