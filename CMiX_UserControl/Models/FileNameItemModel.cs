using CMiX.Services;
using System;

namespace CMiX.Models
{
    [Serializable]
    public class FileNameItemModel : Model
    {
        public FileNameItemModel()
        {

        }

        public string FileName { get; set; }

        public bool FileIsSelected { get; set; }
    }
}
