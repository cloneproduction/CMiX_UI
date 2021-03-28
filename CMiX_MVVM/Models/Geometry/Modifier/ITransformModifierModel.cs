using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Models
{
    public interface ITransformModifierModel : IModel
    {
        string Name { get; set; }
        Guid ID { get; set; }
        int Count { get; set; }
    }
}
