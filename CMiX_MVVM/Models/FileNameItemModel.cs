using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class FileNameItemModel : IModel
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
        public string MessageAddress { get; set; }
    }
}
