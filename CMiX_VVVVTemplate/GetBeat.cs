using VVVV.PluginInterfaces.V2;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels;
using System;
using System.Linq;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "Beat", Category = "CMiX_VVVV")]
    public class GetBeat : IPluginEvaluate
    {
        [Input("IBeat")]
        public IDiffSpread<Composition> Composition;

        [Output("Period")]
        public ISpread<ISpread<double>> FPeriod;

        [Output("Resync")]
        public ISpread<bool> Resync;

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
                        Resync[i] = Composition[i].MasterBeat.Resync.Resynced;
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