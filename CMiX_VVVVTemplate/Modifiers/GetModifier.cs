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
    [PluginInfo(Name = "GetModifier", Category = "CMiX_VVVV")]
    public class GetModifier : IPluginEvaluate
    {
        [Input("Instancer")]
        public IDiffSpread<IModifier> FModifierIn;

        [Output("Uniform")]
        public ISpread<AnimParameter> Uniform;

        [Output("X")]
        public ISpread<AnimParameter> X;

        [Output("Y")]
        public ISpread<AnimParameter> Y;

        [Output("Z")]
        public ISpread<AnimParameter> Z;

        public void Evaluate(int SpreadMax)
        {
            Uniform.SliceCount = FModifierIn.SliceCount;
            X.SliceCount = FModifierIn.SliceCount;
            Y.SliceCount = FModifierIn.SliceCount;
            Z.SliceCount = FModifierIn.SliceCount;

            if (FModifierIn.SliceCount > 0)
            {
                for (int i = 0; i < FModifierIn.SliceCount; i++)
                {
                    if (FModifierIn[i] != null)
                    {
                        Uniform[i] = FModifierIn[i].Uniform;
                        X[i] = FModifierIn[i].X;
                        Y[i] = FModifierIn[i].Y;
                        Z[i] = FModifierIn[i].Z;
                    }
                    else
                    {
                        Uniform.SliceCount = 0;
                        X.SliceCount = 0;
                        Y.SliceCount = 0;
                        Z.SliceCount = 0;
                    }
                }
            }
        }
    }
}
