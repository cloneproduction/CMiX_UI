﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Core.Models
{
    public class LFOModel : Model, IAnimModeModel
    {
        public LFOModel()
        {

        }

        public bool Invert { get; set; }
    }
}
