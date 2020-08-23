using VVVV.PluginInterfaces.V2;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "Beat", Category = "CMiX_VVVV")]
    public class GetBeat : IPluginEvaluate
    {
        [Input("IBeat")]
        public IDiffSpread<Composition> Composition;

        [Output("Period")]
        public ISpread<ISpread<double>> FPeriod;

        [Output("Multiplier")]
        public ISpread<double> FMultiplier;

        public void Evaluate(int SpreadMax)
        {
            FPeriod.SliceCount = Composition.SliceCount;

            if (Composition.SliceCount > 0)
            {
                
                for (int i = 0; i < Composition.SliceCount; i++)
                {
                    FPeriod[i].SliceCount = Composition[i].MasterBeat.Periods.Length;

                    if (Composition[i] != null)
                    {
                        FPeriod[i].AssignFrom(Composition[i].MasterBeat.Periods);
                    }
                    else
                    {
                        FPeriod.SliceCount = 0;
                    }
                }
            }
        }
    }
}