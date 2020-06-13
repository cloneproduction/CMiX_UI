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
            if (FTextureIn.IsChanged)
            {
                if (FTextureIn != null)
                {
                    if (FTextureIn.SliceCount > 0)
                    {
                        FName.SliceCount = FTextureIn.SliceCount;

                        for (int i = 0; i < FTextureIn.SliceCount; i++)
                        {
                            FName[i] = FTextureIn[i].AssetPathSelector.SelectedPath;
                            FLogger.Log(LogType.Debug, "TextureSelectedPath " + FTextureIn[i].AssetPathSelector.SelectedPath);
                        }
                    }
                }
            }
        }
    }
}
