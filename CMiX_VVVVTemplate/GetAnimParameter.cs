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

        [Input("BeatTicks")]
        public IDiffSpread<bool> BeatTicks;

        [Input("Periods")]
        public IDiffSpread<double> Periods;

        [Output("AnimMode")]
        public ISpread<string> AnimMode;

        [Output("RangeMin")]
        public ISpread<double> RangeMin;

        [Output("RangeMax")]
        public ISpread<double> RangeMax;

        [Output("Multiplier")]
        public ISpread<double> Multiplier;

        [Output("ChanceToHit")]
        public ISpread<double> ChanceToHit;

        [Output("Period")]
        public ISpread<double> Period;

        [Output("BeatIndex")]
        public ISpread<int> BeatIndex;

        [Output("Easing")]
        public ISpread<string> Easing;

        [Output("Pass")]
        public ISpread<bool> Pass;

        public Random Random { get; set; }

        public GetAnimParameter()
        {
            Random = new Random();
        }
        public void Evaluate(int SpreadMax)
        {
            AnimMode.SliceCount = AnimParameter.SliceCount;
            Multiplier.SliceCount = AnimParameter.SliceCount;
            ChanceToHit.SliceCount = AnimParameter.SliceCount;
            RangeMin.SliceCount = AnimParameter.SliceCount;
            RangeMax.SliceCount = AnimParameter.SliceCount;
            Easing.SliceCount = AnimParameter.SliceCount;
            Period.SliceCount = AnimParameter.SliceCount;
            BeatIndex.SliceCount = AnimParameter.SliceCount;
            Pass.SliceCount = AnimParameter.SliceCount;

            if (AnimParameter.SliceCount > 0)
            {
                for (int i = 0; i < AnimParameter.SliceCount; i++)
                {
                    if (AnimParameter[i] != null)
                    {
                        AnimMode[i] = AnimParameter[i].AnimMode.GetType().Name;
                        Multiplier[i] = AnimParameter[i].BeatModifier.Multiplier;
                        BeatIndex[i] = AnimParameter[i].BeatModifier.BeatIndex;
                        
                        ChanceToHit[i] = AnimParameter[i].BeatModifier.ChanceToHit.Amount;
                        RangeMin[i] = AnimParameter[i].Range.Minimum;
                        RangeMax[i] = AnimParameter[i].Range.Maximum;
                        Easing[i] = AnimParameter[i].Easing.EasingFunction.ToString() + AnimParameter[i].Easing.EasingMode.ToString();

                        if (BeatTicks[i])
                        {
                            if (Random.NextDouble() * 100 <= AnimParameter[i].BeatModifier.ChanceToHit.Amount)
                                Pass[i] = true;
                            else
                                Pass[i] = false;
                        }

                        if (Pass[i])
                            Period[i] = Periods[AnimParameter[i].BeatModifier.BeatIndex];
                    }
                    else
                    {
                        AnimParameter.SliceCount = 0;
                    }
                }
            }
        }
    }
}