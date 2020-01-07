using System;
using System.Collections.Generic;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class FileSelectorModel
    {
        public FileSelectorModel()
        {
            FilePaths = new List<FileNameItemModel>();
        }

        public string FolderPath { get; set; }
        public List<FileNameItemModel> FilePaths { get; set; }
    }
}