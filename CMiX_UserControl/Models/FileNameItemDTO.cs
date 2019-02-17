using System;

namespace CMiX.Models
{
    [Serializable]
    public class FileNameItemDTO
    {
        public FileNameItemDTO()
        {

        }

        public string FileName { get; set; }
        public bool FileIsSelected { get; set; }
    }
}
