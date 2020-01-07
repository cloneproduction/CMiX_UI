using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class FileNameItemModel
    {
        public FileNameItemModel()
        {

        }

        public string FileName { get; set; }
        public bool FileIsSelected { get; set; }
        public FileNameItemModel SelectedFileNameItem { get; set; }
    }
}
