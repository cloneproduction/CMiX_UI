using System;
using System.Collections.Generic;

namespace CMiX.Models
{
    [Serializable]
    public class FileSelectorModel : Model
    {
        public FileSelectorModel()
        {
            FilePaths = new List<FileNameItemModel>();
            SelectedFileNameItem = new FileNameItemModel();
        }

        public FileSelectorModel(string messageaddress) 
            : this()
        {
            MessageAddress = messageaddress;
        }

        public List<FileNameItemModel> FilePaths { get; set; }

        public FileNameItemModel SelectedFileNameItem { get; set; }
    }
}