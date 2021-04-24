using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Models
{
    public abstract class Model : IModel
    {
        public Guid ID { get; set; }
        public bool Enabled { get; set; }
    }
}
