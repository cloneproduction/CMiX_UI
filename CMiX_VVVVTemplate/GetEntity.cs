using CMiX.MVVM.ViewModels;
using System.Linq;
using VVVV.PluginInterfaces.V2;
using VVVV.Core.Logging;
using System.ComponentModel.Composition;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetEntity", Category = "CMiX_VVVV")]
    public class GetEntity : IPluginEvaluate
    {
        [Import()]
        public ILogger FLogger;

        [Input("Component")]
        public IDiffSpread<Scene> FComponentIn;

        [Output("Entity")]
        public ISpread<ISpread<Entity>> FEntityOut;

        public void Evaluate(int SpreadMax)
        {
            FEntityOut.SliceCount = FComponentIn.SliceCount;

            if (FComponentIn.SliceCount > 0)
            {
                for (int i = 0; i < FComponentIn.SliceCount; i++)
                {
                    if (FComponentIn[i] != null)
                    {
                        FEntityOut[i].AssignFrom(FComponentIn[i].Components.Cast<Entity>());
                    }
                        
                    else
                        FEntityOut[i].SliceCount = 0;
                }
            }
        }
    }
}
