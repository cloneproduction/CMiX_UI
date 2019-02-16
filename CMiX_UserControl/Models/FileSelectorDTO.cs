using System;
using System.Collections.Generic;

namespace CMiX.Models
{
    [Serializable]
    public class FileSelectorDTO
    {
        public FileSelectorDTO()
        {
            FilePaths = new List<FileNameItemDTO>();
        }

        public List<FileNameItemDTO> FilePaths { get; set; }
    }
}