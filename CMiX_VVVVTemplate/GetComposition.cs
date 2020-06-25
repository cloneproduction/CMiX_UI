using CMiX.MVVM.ViewModels;
using System.ComponentModel.Composition;
using System.Linq;
using VVVV.Core.Logging;
using VVVV.PluginInterfaces.V2;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetComposition", Category = "CMiX_VVVV")]
    public class GetComposition : IPluginEvaluate
    {
        [Import()]
        public ILogger FLogger;

        [Input("Project", IsSingle = true)]
        public ISpread<Project> FProjectIn;

        [Output("Composition")]
        public ISpread<ISpread<Composition>> FCompositionOut;

        public void Evaluate(int SpreadMax)
        {
            FCompositionOut.SliceCount = FProjectIn.SliceCount;
            if (FProjectIn.SliceCount > 0)
            {
                for (int i = 0; i < FProjectIn.SliceCount; i++)
                {
                    if (FProjectIn[i] != null)
                        FCompositionOut[i].AssignFrom(FProjectIn[i].Components.Cast<Composition>());
                    else
                        FCompositionOut[i].SliceCount = 0;
                }
            }
        }
    }
}
