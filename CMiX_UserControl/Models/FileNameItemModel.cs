using System;

namespace CMiX.Models
{
    [Serializable]
    public class FileNameItemModel : Model
    {
        public FileNameItemModel()
        {

        }

        public FileNameItemModel(string messageaddress)
            : this()
        {
            MessageAddress = messageaddress;
        }

        public string FileName { get; set; }

        public bool FileIsSelected { get; set; }

        public FileNameItemModel SelectedFileNameItem { get; set; }
    }
}
