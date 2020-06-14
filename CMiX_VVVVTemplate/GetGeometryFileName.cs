using CMiX.MVVM.ViewModels;
using System.ComponentModel.Composition;
using VVVV.Core.Logging;
using VVVV.PluginInterfaces.V2;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetGeometryFileName", Category = "CMiX_VVVV")]
    public class GetGeometryFileName : IPluginEvaluate
    {
        [Import()]
        public ILogger FLogger;

        [Input("Geometry")]
        public IDiffSpread<Geometry> FGeometryIn;

        [Output("FileName")]
        public ISpread<string> FName;

        public void Evaluate(int SpreadMax)
        {
            FName.SliceCount = FGeometryIn.SliceCount;
            if (FGeometryIn.SliceCount > 0)
            {
                for (int i = 0; i < FGeometryIn.SliceCount; i++)
                {
                    if (FGeometryIn[i] != null)
                        FName[i] = FGeometryIn[i].AssetPathSelector.SelectedPath;
                    else
                        FGeometryIn.SliceCount = 0;
                }
            }
        }
    }
}
