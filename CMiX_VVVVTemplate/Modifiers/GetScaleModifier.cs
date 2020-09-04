using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVVV.PluginInterfaces.V2;

namespace VVVV.Nodes.Modifiers
{
    [PluginInfo(Name = "GetScaleModifier", Category = "CMiX_VVVV")]
    public class GetScaleModifier : IPluginEvaluate
    {
        [Input("Instancer")]
        public IDiffSpread<ScaleModifier> FScaleModifierIn;

        [Output("Uniform")]
        public ISpread<AnimParameter> Uniform;

        public void Evaluate(int SpreadMax)
        {
            Uniform.SliceCount = FScaleModifierIn.SliceCount;

            if (FScaleModifierIn.SliceCount > 0)
            {
                for (int i = 0; i < FScaleModifierIn.SliceCount; i++)
                {
                    if (FScaleModifierIn[i] != null)
                    {
                        Uniform[i] = FScaleModifierIn[i].Uniform;
                    }
                    else
                    {
                        Uniform.SliceCount = 0;
                    }
                }
            }
        }
    }
}
