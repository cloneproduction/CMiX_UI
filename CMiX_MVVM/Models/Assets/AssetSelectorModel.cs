﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models
{
    public interface IAssetSelectorModel
    {
        IAssetModel SelectedAssetModel { get; set; }
    }
}
