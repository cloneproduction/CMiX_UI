using System.Collections.ObjectModel;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;

namespace CMiX.MVVM.ViewModels 
{
    public class FileNameItem : ViewModel, ISendable
    {
        #region CONSTRUCTORS
        public FileNameItem()
        {

        }

        public FileNameItem(string messageaddress, Sender sender)
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
        public Sender Sender { get; set; }
        #endregion

        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress + "Selected";
        }

        #region COPY/PASTE/UPDATE
        public void Copy(FileNameItemModel filenameitemmodel)
        {
            filenameitemmodel.MessageAddress = MessageAddress;
            filenameitemmodel.FileIsSelected = FileIsSelected;
            filenameitemmodel.FileName = FileName;
        }

        public void Paste(FileNameItemModel filenameitemmodel)
        {
            Sender.Enable();

            MessageAddress = filenameitemmodel.MessageAddress;
            FileName = filenameitemmodel.FileName;
            FileIsSelected = filenameitemmodel.FileIsSelected;

            Sender.Enable();
        }
        #endregion
    }
}