using CMiX.MVVM.ViewModels;
using System.ComponentModel.Composition;
using VVVV.Core.Logging;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VMath;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetGeometryTransform", Category = "CMiX_VVVV")]
    public class GetGeometryTransform : IPluginEvaluate
    {
        [Import()]
        public ILogger FLogger;

        [Input("Entity")]
        public IDiffSpread<Geometry> FGeometryIn;

        [Output("Transform")]
        public ISpread<Matrix4x4> FTransform;

        public void Evaluate(int SpreadMax)
        {
            if (FGeometryIn.SliceCount > 0)
            {
                FTransform.SliceCount = FGeometryIn.SliceCount;

                for (int i = 0; i < FGeometryIn.SliceCount; i++)
                {
                    if(FGeometryIn[i] != null)
                    {
                        var transform = FGeometryIn[i].Transform;

                        var uniform = VMath.Scale
                        (
                            transform.Scale.Uniform.Amount,
                            transform.Scale.Uniform.Amount,
                            transform.Scale.Uniform.Amount
                        );

                        var translate = VMath.Translate
                        (
                            transform.Translate.X.Amount,
                            transform.Translate.Y.Amount,
                            transform.Translate.Z.Amount
                        );

                        var scale = VMath.Scale
                        (
                            transform.Scale.X.Amount,
                            transform.Scale.Y.Amount,
                            transform.Scale.Z.Amount
                        );

                        var rotation = VMath.Rotate
                        (
                            transform.Rotation.X.Amount,
                            transform.Rotation.Y.Amount,
                            transform.Rotation.Z.Amount
                        );

                        FTransform[i] = scale * uniform * rotation * translate;
                    }
                    else
                    {
                        FTransform.SliceCount = 0;
                    }
                }
            }
        }
    }
}
