﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public interface IParameter
    {
        double[] Values { get; set; }
        int Count { get; set; }
    }
}