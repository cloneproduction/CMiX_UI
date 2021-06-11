﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public interface IRange
    {
        double Minimum { get; set; }
        double Maximum { get; set; }
        double Width { get; set; }
    }
}
