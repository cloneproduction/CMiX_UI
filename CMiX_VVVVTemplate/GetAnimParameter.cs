using CMiX.MVVM.ViewModels;
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

        [Output("Influence")]
        public ISpread<double> Influence;

        [Output("Mode")]
        public ISpread<string> Mode;

        [Output("Multiplier")]
        public ISpread<double> Multiplier;

        [Output("ChanceToHit")]
        public ISpread<double> ChanceToHit;

        public void Evaluate(int SpreadMax)
        {
            Influence.SliceCount = AnimParameter.SliceCount;
            Mode.SliceCount = AnimParameter.SliceCount;
            Multiplier.SliceCount = AnimParameter.SliceCount;
            ChanceToHit.SliceCount = AnimParameter.SliceCount;


            if (AnimParameter.SliceCount > 0)
            {
                for (int i = 0; i < AnimParameter.SliceCount; i++)
                {
                    if (AnimParameter[i] != null)
                    {
                        Influence[i] = AnimParameter[i].Influence.Amount;
                        Mode[i] = AnimParameter[i].Mode.Mode.ToString();
                        Multiplier[i] = AnimParameter[i].BeatModifier.Multiplier;
                        ChanceToHit[i] = AnimParameter[i].BeatModifier.ChanceToHit.Amount;
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
