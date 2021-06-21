using CMiX.Core.Models;
using System;

namespace CMiX.Core.Models
{
    public interface IRangeModel : IModel
    {
        Guid ID { get; set; }
        double Minimum { get; set; }
        double Maximum { get; set; }
    }
}