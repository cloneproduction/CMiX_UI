using CMiX.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public IDiffSpread<Project> FProjectIn;

        [Output("Composition")]
        public ISpread<ISpread<Composition>> FCompositionOut;

        public void Evaluate(int SpreadMax)
        {
            if (FProjectIn.IsChanged)
            {
                if (FProjectIn != null)
                {
                    if (FProjectIn.SliceCount > 0)
                    {
                        FCompositionOut.SliceCount = FProjectIn.SliceCount;
                        for (int i = 0; i < FProjectIn.SliceCount; i++)
                        {
                            FCompositionOut[i].AssignFrom(FProjectIn[i].Components.Cast<Composition>());
                        }
                    }
                }
            }
        }
    }
}
