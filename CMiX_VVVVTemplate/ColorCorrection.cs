using CMiX.MVVM.ViewModels;
using System.ComponentModel.Composition;
using VVVV.Core.Logging;
using VVVV.PluginInterfaces.V2;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "ColorCorrection", Category = "CMiX_VVVV")]
    public class ColorCorrection : IPluginEvaluate
    {
        [Import()]
        public ILogger FLogger;

        [Input("Entity")]
        public IDiffSpread<Texture> FTextureIn;

        [Output("Brightness")]
        public ISpread<double> FBrightness;

        [Output("Contrast")]
        public ISpread<double> FContrast;

        [Output("Hue")]
        public ISpread<double> FHue;

        [Output("Sat")]
        public ISpread<double> FSaturation;

        [Output("Luminosity")]
        public ISpread<double> FLuminosity;

        [Output("Keying")]
        public ISpread<double> FKeying;

        public void Evaluate(int SpreadMax)
        {
            FBrightness.SliceCount = FTextureIn.SliceCount;
            FContrast.SliceCount = FTextureIn.SliceCount;
            FHue.SliceCount = FTextureIn.SliceCount;
            FSaturation.SliceCount = FTextureIn.SliceCount;
            FLuminosity.SliceCount = FTextureIn.SliceCount;
            FKeying.SliceCount = FTextureIn.SliceCount;

            if (FTextureIn.SliceCount > 0)
            {
                for (int i = 0; i < FTextureIn.SliceCount; i++)
                {
                    if (FTextureIn[i] != null)
                    {
                        FBrightness[i] = FTextureIn[i].Brightness.Amount;
                        FContrast[i] = FTextureIn[i].Contrast.Amount;
                        FLuminosity[i] = FTextureIn[i].Luminosity.Amount;
                        FSaturation[i] = FTextureIn[i].Saturation.Amount;
                        FHue[i] = FTextureIn[i].Hue.Amount;
                        FKeying[i] = FTextureIn[i].Keying.Amount;
                    }
                    else
                        FTextureIn.SliceCount = 0;
                }
            }
        }
    }
}
