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

        public string MessageAddress { get; set; }

        //[OSC]
        public List<FileNameItemModel> FilePaths { get; set; }

        [OSC]
        public FileNameItemModel SelectedFileNameItem { get; set; }
    }
}