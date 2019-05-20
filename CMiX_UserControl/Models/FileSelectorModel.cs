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
        }

        public List<FileNameItemModel> FilePaths { get; set; }
    }
}