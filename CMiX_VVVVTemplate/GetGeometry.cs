using CMiX.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVVV.PluginInterfaces.V2;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetGeometry", Category = "CMiX_VVVV")]
    public class GetGeometry : IPluginEvaluate
    {
        [Input("Entity")]
        public IDiffSpread<Entity> FComponentIn;

        [Output("Geometry")]
        public ISpread<Geometry> FGeometry;

        public void Evaluate(int SpreadMax)
        {
            FGeometry.SliceCount = FComponentIn.SliceCount;
            if (FComponentIn.SliceCount > 0)
            {
                for (int i = 0; i < FComponentIn.SliceCount; i++)
                {
                    if (FComponentIn[i] != null)
                        FGeometry[i] = FComponentIn[i].Geometry;
                    else
                        FGeometry.SliceCount = 0;
                }
            }
        }
    }
}
