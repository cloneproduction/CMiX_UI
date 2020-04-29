using System.Collections.ObjectModel;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;

namespace CMiX.MVVM.ViewModels 
{
    public class FileNameItem : ViewModel
    {
        #region CONSTRUCTORS
        public FileNameItem()
        {

        }

        public FileNameItem(string messageaddress, MessageService messageService)
        {
            MessageAddress = messageaddress + "Selected";
        }
        #endregion

        #region PROPERTIES
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
                    //SendMessages(MessageAddress, FileName);
                }
            }
        }

        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }
        #endregion

        #region COPY/PASTE/UPDATE
        public void Copy(FileNameItemModel filenameitemmodel)
        {
            filenameitemmodel.FileIsSelected = FileIsSelected;
            filenameitemmodel.FileName = FileName;
        }

        public void Paste(FileNameItemModel filenameitemmodel)
        {
            MessageService.Enable();

            FileName = filenameitemmodel.FileName;
            FileIsSelected = filenameitemmodel.FileIsSelected;

            MessageService.Enable();
        }
        #endregion
    }
}