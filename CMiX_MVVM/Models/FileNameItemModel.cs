﻿using System;

namespace CMiX.Core.Models
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
