using System;

namespace CMiX.ViewModels 
{
    public class FileNameItem : ViewModel
    {
        public FileNameItem()
        {
            MessageAddress = string.Empty;
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
    }
}