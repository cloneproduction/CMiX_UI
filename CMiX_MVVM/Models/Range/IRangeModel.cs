using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.Models
{
    public interface IRangeModel : IModel
    {
        double Minimum { get; set; }
        double Maximum { get; set; }
    }
}