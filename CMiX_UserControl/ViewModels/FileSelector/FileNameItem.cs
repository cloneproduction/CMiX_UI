using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class FileNameItem : ViewModel
    {
        public FileNameItem(string folderpath, string messageAddress, MessengerService messengerService)
        {
            MessageAddress = messageAddress + "Selected";
            MessengerService = messengerService;
            FolderPath = folderpath;

        }

        public string MessageAddress { get; set; }
        public MessengerService MessengerService { get; set; }

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
        //public FileNameItemModel GetModel()
        //{
        //    FileNameItemModel fileNameItemModel = new FileNameItemModel();
        //    fileNameItemModel.FileIsSelected = FileIsSelected;
        //    fileNameItemModel.FileName = FileName;
        //    return fileNameItemModel;
        //}

        //public void CopyModel(FileNameItemModel filenameitemmodel)
        //{
        //    filenameitemmodel.FileIsSelected = FileIsSelected;
        //    filenameitemmodel.FileName = FileName;
        //}

        public void SetViewModel(FileNameItemModel filenameitemmodel)
        {
            MessengerService.Disable();

            FileIsSelected = filenameitemmodel.FileIsSelected;
            FileName = filenameitemmodel.FileName;

            MessengerService.Enable();
        }
        #endregion
    }
}