using CMiX.Services;
using System;
using System.Collections.Generic;

namespace CMiX.Models
{
    [Serializable]
    public class FileSelectorModel
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

        public string MessageAddress { get; set; }

        public List<FileNameItemModel> FilePaths { get; set; }

        [OSC]
        public FileNameItemModel SelectedFileNameItem { get; set; }
    }
}