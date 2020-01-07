using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class FileNameItem : ViewModel, ISendable
    {
        public FileNameItem(string folderpath, string messageAddress, Sender sender)
        {
            MessageAddress = messageAddress + "Selected";
            Sender = sender;
            FolderPath = folderpath;

        }

        public string MessageAddress { get; set; }
        public Sender Sender { get; set; }

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


        #region COPY/PASTE
        public void CopyModel(FileNameItemModel filenameitemmodel)
        {
            filenameitemmodel.FileIsSelected = FileIsSelected;
            filenameitemmodel.FileName = FileName;
        }

        public void PasteModel(FileNameItemModel filenameitemmodel)
        {
            Sender.Disable();

            FileIsSelected = filenameitemmodel.FileIsSelected;
            FileName = filenameitemmodel.FileName;

            Sender.Enable();
        }
        #endregion
    }
}