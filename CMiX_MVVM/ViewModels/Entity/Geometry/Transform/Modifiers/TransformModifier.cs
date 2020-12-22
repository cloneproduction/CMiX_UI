using System.Windows.Media.Media3D;

namespace CMiX.MVVM.ViewModels
{
    public interface ITransformModifier
    {
        int Count { get; set; }
        Vector3D[] TranslateXYZ { get; set; }
        Vector3D[] ScaleXYZ { get; set; }
        Vector3D[] RotationXYZ { get; set; }
    }
}