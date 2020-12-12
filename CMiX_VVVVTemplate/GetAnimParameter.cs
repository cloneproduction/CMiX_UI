using CMiX.MVVM.ViewModels;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using VVVV.Core.Logging;
using VVVV.PluginInterfaces.V2;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetAnimParameter", AutoEvaluate =true, Category = "CMiX_VVVV")]
    public class GetAnimParameter : IPluginEvaluate
    {
        [Import()]
        public ILogger FLogger;

        [Input("AnimParameter")]
        public IDiffSpread<AnimParameter> AnimParameter;

        [Input("Periods")]
        public IDiffSpread<double> Periods;

        [Input("BeatTicks")]
        public IDiffSpread<bool> BeatTicks;

        [Output("Parameters")]
        public ISpread<ISpread<double>> Parameters;

        public void Evaluate(int SpreadMax)
        {
            Parameters.SliceCount = AnimParameter.SliceCount;

            if (AnimParameter.SliceCount > 0)
            {
                for (int i = 0; i < AnimParameter.SliceCount; i++)
                {
                    if (AnimParameter[i] != null)
                    {
                        AnimParameter[i].Period = Periods.ToArray();

                        Parameters[i].SliceCount = AnimParameter[i].Parameters.Length;

                        if (BeatTicks[AnimParameter[i].BeatModifier.BeatIndex])
                        {
                            AnimParameter[i].AnimateOnBeatTick();
                        }
                            
                        AnimParameter[i].AnimateOnGameLoop();

                        for (int j = 0; j < AnimParameter[i].Parameters.Length; j++)
                        {
                            Parameters[i][j] = AnimParameter[i].Parameters[j];
                        }
                    }
                    else
                        AnimParameter.SliceCount = 0;
                }
            }
        }
    }
}