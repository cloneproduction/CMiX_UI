using CMiX.MVVM.Resources;
using CMiX.MVVM.ViewModels;
using System;
using System.ComponentModel.Composition;
using VVVV.Core.Logging;
using VVVV.PluginInterfaces.V2;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetAnimParameter", Category = "CMiX_VVVV")]
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

        [Output("Period")]
        public ISpread<double> Period;

        [Output("Pass")]
        public ISpread<bool> Pass;

        public Random Random { get; set; }

        public GetAnimParameter()
        {
            Random = new Random();
        }

        private double map(double value, double fromLow, double fromHigh, double toLow, double toHigh)
        {
            return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        }

        public void Evaluate(int SpreadMax)
        {
            Period.SliceCount = AnimParameter.SliceCount;
            Pass.SliceCount = AnimParameter.SliceCount;

            if (AnimParameter.SliceCount > 0)
            {
                for (int i = 0; i < AnimParameter.SliceCount; i++)
                {
                    if (AnimParameter[i] != null)
                    {
                        //AnimMode[i] = AnimParameter[i].SelectedModeType.ToString();

                        if (BeatTicks[i])
                        {
                            if (Random.NextDouble() * 100 <= AnimParameter[i].BeatModifier.ChanceToHit.Amount)
                                Pass[i] = true;
                            else
                                Pass[i] = false;
                        }

                        if (BeatTicks[AnimParameter[i].BeatModifier.BeatIndex])
                            AnimParameter[i].OnBeatTick.Invoke(Periods[AnimParameter[i].BeatModifier.BeatIndex]);

                        if (Pass[i])
                            Period[i] = AnimParameter[i].OnUpdatePeriod.Invoke(Periods[AnimParameter[i].BeatModifier.BeatIndex]);
                    }
                    else
                        AnimParameter.SliceCount = 0;
                }
            }
        }
    }
}