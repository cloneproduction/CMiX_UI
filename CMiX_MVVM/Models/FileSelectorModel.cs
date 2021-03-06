﻿using System;
using System.Collections.Generic;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class FileSelectorModel : Model
    {
        public FileSelectorModel()
        {
            FilePaths = new List<FileNameItemModel>();
            //SelectedFileNameItem = new FileNameItemModel();
        }

        public FileSelectorModel(string messageaddress) 
            : this()
        {
            MessageAddress = messageaddress;
        }

        public string FolderPath { get; set; }

        public List<FileNameItemModel> FilePaths { get; set; }

        //public FileNameItemModel SelectedFileNameItem { get; set; }
    }
}