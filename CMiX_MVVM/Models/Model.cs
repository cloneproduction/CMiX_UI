using CMiX.Core.Models;
using System;

namespace CMiX.Core.Models
{
    public abstract class Model : IModel
    {
        public Guid ID { get; set; }
        public bool Enabled { get; set; }
    }
}
