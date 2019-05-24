using System;
using CMiX.Models;
using Memento;

namespace CMiX.ViewModels 
{
    public class FileNameItem : ViewModel
    {
        public FileNameItem(Mementor mementor)
        {
            MessageAddress = string.Empty;
            Mementor = mementor;
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
            set => SetAndNotify(ref _fileisselected, value);
        }

        #region COPY/PASTE
        public void Copy(FileNameItemModel filenameitemmodel)
        {
            filenameitemmodel.FileIsSelected = FileIsSelected;
            filenameitemmodel.FileName = FileName;
        }

        public void Paste(FileNameItemModel filenameitemmodel)
        {

            DisabledMessages();

            FileIsSelected = filenameitemmodel.FileIsSelected;
            FileName = filenameitemmodel.FileName;

            EnabledMessages();
        }
        #endregion
    }
}