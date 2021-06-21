using CMiX.Core.Models;
using CMiX.Core.Presentation.ViewModels;
using System;

namespace CMiX.Core.Models
{
    public interface ITransformModifierModel : IModel
    {
        TransformModifierNames Name { get; set; }
        Guid ID { get; set; }
        int Count { get; set; }
    }
}
