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
            if (FComponentIn.IsChanged)
            {
                if (FComponentIn != null)
                {
                    if (FComponentIn.SliceCount > 0)
                    {
                        FSceneOut.SliceCount = FComponentIn.SliceCount;
                        //FFade.SliceCount = FComponentIn.SliceCount;
                        //FBlendMode.SliceCount = FComponentIn.SliceCount;

                        for (int i = 0; i < FComponentIn.SliceCount; i++)
                        {
                            FSceneOut[i].AssignFrom(FComponentIn[i].Components.Cast<Scene>());

                            //FFade[i].SliceCount = FLayerOut[i].SliceCount;
                            //FBlendMode[i].SliceCount = FLayerOut[i].SliceCount;

                            for (int j = 0; j < FSceneOut[i].SliceCount; j++)
                            {
                                //FFade[i][j] = FLayerOut[i][j].Fade.Amount;
                                //FBlendMode[i][j] = FLayerOut[i][j].BlendMode.Mode;
                            }
                        }
                    }
                }
            }
        }
    }
}
