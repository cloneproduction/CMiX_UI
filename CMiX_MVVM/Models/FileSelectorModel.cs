using System;
using System.Collections.Generic;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class FileSelectorModel : IModel
    {
        public FileSelectorModel()
        {
            FilePaths = new List<FileNameItemModel>();
        }

        public FileSelectorModel(string messageaddress) 
            : this()
        {
            MessageAddress = messageaddress;
        }

        public string FolderPath { get; set; }

        public List<FileNameItemModel> FilePaths { get; set; }
        public string MessageAddress { get; set; }
    }
}