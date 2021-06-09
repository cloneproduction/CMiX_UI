using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.Models
{
    public interface ITransformModifierModel : IModel
    {
        TransformModifierNames Name { get; set; }
        Guid ID { get; set; }
        int Count { get; set; }
    }
}
