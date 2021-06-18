using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Models
{
    public interface IRangeModel : IModel
    {
        Guid ID { get; set; }
        double Minimum { get; set; }
        double Maximum { get; set; }
    }
}