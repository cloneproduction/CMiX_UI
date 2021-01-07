using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;

namespace CMiX.MVVM.ViewModels
{
    public interface ITransformModifier
    {
        string Name { get; set; }
        ObservableCollection<Transform> Transforms { get; set; }
        Vector3D[] Location { get; set; }
        Vector3D[] Scale { get; set; }
        Vector3D[] Rotation { get; set; }
    }
}