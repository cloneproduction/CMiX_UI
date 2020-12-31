using System.Windows.Media.Media3D;

namespace CMiX.MVVM.ViewModels
{
    public interface ITransformModifier
    {
        string Name { get; set; }
        int Count { get; set; }
        Vector3D[] Location { get; set; }
        Vector3D[] Scale { get; set; }
        Vector3D[] Rotation { get; set; }
    }
}