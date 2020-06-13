using CMiX.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVVV.PluginInterfaces.V2;
using VVVV.Core.Logging;
using System.ComponentModel.Composition;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetEntity", Category = "CMiX_VVVV")]
    public class GetEntity : IPluginEvaluate
    {
        [Import()]
        public ILogger FLogger;

        [Input("Component")]
        public IDiffSpread<Scene> FComponentIn;

        [Output("Entity")]
        public ISpread<ISpread<Entity>> FEntityOut;

        public void Evaluate(int SpreadMax)
        {
            if (FComponentIn.IsChanged)
            {
                if (FComponentIn != null)
                {
                    if (FComponentIn.SliceCount > 0)
                    {
                        FEntityOut.SliceCount = FComponentIn.SliceCount;
                        //FFade.SliceCount = FComponentIn.SliceCount;
                        //FBlendMode.SliceCount = FComponentIn.SliceCount;

                        for (int i = 0; i < FComponentIn.SliceCount; i++)
                        {
                            FEntityOut[i].AssignFrom(FComponentIn[i].Components.Cast<Entity>());

                            //FFade[i].SliceCount = FLayerOut[i].SliceCount;
                            //FBlendMode[i].SliceCount = FLayerOut[i].SliceCount;

                            for (int j = 0; j < FEntityOut[i].SliceCount; j++)
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
