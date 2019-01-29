using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CMiX.ViewModels 
{
    public class FileNameItem : ViewModel
    {
        public FileNameItem()
        {
            DeleteItemCommand = new RelayCommand(p => DeleteItem());
        }

        private void DeleteItem()
        {
            Console.WriteLine("DeleteItem");
        }

        public ICommand DeleteItemCommand { get; }

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

        private bool _fileisdeleted;
        public bool FileIsDeleted
        {
            get => _fileisdeleted;
            set => SetAndNotify(ref _fileisdeleted, value);
        }
    }
}
