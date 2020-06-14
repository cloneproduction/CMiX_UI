using CMiX.MVVM.ViewModels;
using System.ComponentModel.Composition;
using System.Linq;
using VVVV.Core.Logging;
using VVVV.PluginInterfaces.V2;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetScene", Category = "CMiX_VVVV")]
    public class GetScene : IPluginEvaluate
    {
        [Import()]
        public ILogger FLogger;

        [Input("Layer")]
        public IDiffSpread<Layer> FComponentIn;

        [Output("Scene")]
        public ISpread<ISpread<Scene>> FSceneOut;

        public void Evaluate(int SpreadMax)
        {
            FSceneOut.SliceCount = FComponentIn.SliceCount;
            if (FComponentIn.SliceCount > 0)
            {
                for (int i = 0; i < FComponentIn.SliceCount; i++)
                {
                    if (FComponentIn[i] != null)
                        FSceneOut[i].AssignFrom(FComponentIn[i].Components.Cast<Scene>());
                    else
                        FSceneOut[i].SliceCount = 0;
                }
            }
        }
    }
}
