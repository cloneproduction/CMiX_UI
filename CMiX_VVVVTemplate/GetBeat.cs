using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVVV.PluginInterfaces.V2;
using CMiX.MVVM.Interfaces;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "Beat", Category = "CMiX_VVVV")]
    public class GetBeat : IPluginEvaluate
    {
        [Input("IBeat")]
        public ISpread<IBeat> FBeat;

        [Output("Period")]
        public ISpread<double> FPeriod;

        [Output("Multiplier")]
        public ISpread<double> FMultiplier;

        [Output("BeatTick")]
        public ISpread<int> FBeatTick;

        public void Evaluate(int SpreadMax)
        {
            FPeriod.SliceCount = FBeat.SliceCount;
            FMultiplier.SliceCount = FBeat.SliceCount;
            FBeatTick.SliceCount = FBeat.SliceCount;

            if (FBeat.SliceCount > 0)
            {
                for (int i = 0; i < FBeat.SliceCount; i++)
                {
                    if (FBeat[i] != null)
                    {
                        FBeatTick[i] = FBeat[i].Beat.BeatTick;
                        FPeriod[i] = FBeat[i].Beat.Period;
                        FMultiplier[i] = FBeat[i].Beat.Multiplier;
                    }
                    else
                    {
                        FBeatTick.SliceCount = 0;
                        FPeriod.SliceCount = 0;
                        FMultiplier.SliceCount = 0;
                    }
                }
            }
        }
    }
}