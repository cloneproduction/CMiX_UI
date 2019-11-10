using System.Collections.ObjectModel;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.MVVM.ViewModels 
{
    public class FileNameItem : ViewModel
    {
        #region CONSTRUCTORS
        public FileNameItem()
        {

        }

        public FileNameItem(string messageaddress, ObservableCollection<ServerValidation> serverValidations, Mementor mementor)
            : base (serverValidations, mementor)
        {
            MessageAddress = messageaddress + "Selected";
            Mementor = mementor;
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
        #endregion

        #region COPY/PASTE/UPDATE
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress + "Selected";
        }

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
            FileName = filenameitemmodel.FileName;
            FileIsSelected = filenameitemmodel.FileIsSelected;

            EnabledMessages();
        }
        #endregion
    }
}