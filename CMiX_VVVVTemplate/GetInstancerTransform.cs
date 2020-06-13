using CMiX.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVVV.Core.Logging;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VMath;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetInstancerTransform", Category = "CMiX_VVVV")]
    public class GetInstancerTransform : IPluginEvaluate
    {

        [Import()]
        public ILogger FLogger;

        [Input("Entity")]
        public IDiffSpread<Instancer> FInstancerIn;

        [Output("Transform")]
        public ISpread<Matrix4x4> FTransform;


        public void Evaluate(int SpreadMax)
        {
            if (FInstancerIn.IsChanged)
            {
                if (FInstancerIn != null)
                {
                    if (FInstancerIn.SliceCount > 0)
                    {
                        FTransform.SliceCount = FInstancerIn.SliceCount;

                        for (int i = 0; i < FInstancerIn.SliceCount; i++)
                        {
                            var transform = FInstancerIn[i].Transform;

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

                            FTransform[i] = scale * rotation * translate;
                            FLogger.Log(LogType.Debug, "transform.Rotation.X.Amount " + transform.Rotation.X.Amount.ToString());
                            FLogger.Log(LogType.Debug, "transform.Rotation.Y.Amount " + transform.Rotation.Y.Amount.ToString());
                            FLogger.Log(LogType.Debug, "transform.Rotation.Z.Amount " + transform.Rotation.Z.Amount.ToString());
                        }
                    }
                }
            }
        }
    }
}
