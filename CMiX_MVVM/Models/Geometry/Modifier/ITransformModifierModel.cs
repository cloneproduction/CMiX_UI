using CMiX.MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models
{
    public interface ITransformModifierModel : IModel
    {
        string Name { get; set; }
        int ID { get; set; }
        int Count { get; set; }
    }
}
