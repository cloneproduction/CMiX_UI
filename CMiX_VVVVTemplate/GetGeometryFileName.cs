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
            if (FGeometryIn.IsChanged)
            {
                if (FGeometryIn != null)
                {
                    if (FGeometryIn.SliceCount > 0)
                    {
                        FName.SliceCount = FGeometryIn.SliceCount;

                        for (int i = 0; i < FGeometryIn.SliceCount; i++)
                        {
                            FName[i] = FGeometryIn[i].AssetPathSelector.SelectedPath;
                            FLogger.Log(LogType.Debug, "GeometrySelectedPath " +  FGeometryIn[i].AssetPathSelector.SelectedPath);
                        }
                    }
                }
            }
        }
    }
}
