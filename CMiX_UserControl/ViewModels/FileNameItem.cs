using System;
using System.Collections.ObjectModel;
using CMiX.Models;
using CMiX.Services;
using Memento;

namespace CMiX.ViewModels 
{
    public class FileNameItem : ViewModel
    {
        public FileNameItem(string messageaddress, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor)
            : base (oscmessengers, mementor)
        {
            MessageAddress = messageaddress;
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
            set
            {
                SetAndNotify(ref _fileisselected, value);
                if (FileIsSelected)
                {
                    SendMessages(MessageAddress + "SelectedFileNameItem", FileName);
                }
            }
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