using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels;
using System.ComponentModel.Composition;
using VVVV.Core.Logging;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VMath;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetTransform", Category = "CMiX_VVVV")]
    public class GetTransform : IPluginEvaluate
    {
        [Import()]
        public ILogger FLogger;

        [Input("Entity")]
        public IDiffSpread<ITransform> ITransformIn;

        [Output("Transform")]
        public ISpread<Matrix4x4> FTransform;

        public void Evaluate(int SpreadMax)
        {
            FTransform.SliceCount = ITransformIn.SliceCount;
            if (ITransformIn.SliceCount > 0)
            {
                

                for (int i = 0; i < ITransformIn.SliceCount; i++)
                {
                    if(ITransformIn[i] != null)
                    {
                        var transform = ITransformIn[i].Transform;

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
