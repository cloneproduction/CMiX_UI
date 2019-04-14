using System;
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
            set => SetAndNotify(ref _filename, Utils.GetRelativePath(@"D:\", value));
        }

        private bool _fileisselected;
        public bool FileIsSelected
        {
            get => _fileisselected;
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, "FileIsSelected");
                SetAndNotify(ref _fileisselected, value);
            }
        }
    }
}