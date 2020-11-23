using CMiX.MVVM.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using VVVV.Core.Logging;
using VVVV.PluginInterfaces.V2;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetSlider", Category = "CMiX_VVVV")]
    public class GetSlider : IPluginEvaluate
    {
        [Import()]
        public ILogger FLogger;

        [Input("Layer")]
        public IDiffSpread<Layer> FComponentIn;

        [Output("Amount")]
        public ISpread<double> FFade;

        public void Evaluate(int SpreadMax)
        {
            FFade.SliceCount = FComponentIn.SliceCount;

            if (FComponentIn.SliceCount > 0)
            {
                for (int i = 0; i < FComponentIn.SliceCount; i++)
                {
                    if (FComponentIn[i] != null)
                    {
                        FFade.SliceCount = FComponentIn.SliceCount;
                        FFade[i] = ((Layer)FComponentIn[i]).Fade.Amount;
                    }
                    else
                    {
                        FFade.SliceCount = 0;
                    }
                }
            }
        }
    }
}
