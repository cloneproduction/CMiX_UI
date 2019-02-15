using System;
using System.Collections.Generic;
using CMiX.ViewModels;

namespace CMiX.Models
{
    [Serializable]
    public class FileSelectorDTO
    {
        public FileSelectorDTO()
        {
            FilePaths = new List<FileNameItem>();
        }

        public List<FileNameItem> FilePaths { get; set; }
    }
}