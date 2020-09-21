using CMiX.MVVM.ViewModels;
using System.ComponentModel.Composition;
using VVVV.Core.Logging;
using VVVV.PluginInterfaces.V2;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetTextureFileName", Category = "CMiX_VVVV")]
    public class GetTextureFileName : IPluginEvaluate
    {
        [Import()]
        public ILogger FLogger;

        [Input("Entity")]
        public IDiffSpread<Texture> FTextureIn;

        [Output("FileName")]
        public ISpread<string> FName;

        public void Evaluate(int SpreadMax)
        {
            FName.SliceCount = FTextureIn.SliceCount;
            if (FTextureIn.SliceCount > 0)
            {
                for (int i = 0; i < FTextureIn.SliceCount; i++)
                {
                    if (FTextureIn[i] != null && FTextureIn[i].AssetPathSelector.SelectedAsset != null)
                        FName[i] = FTextureIn[i].AssetPathSelector.SelectedAsset.Path;
                    else
                        FTextureIn.SliceCount = 0;
                }
            }
        }
    }
}