using CMiX.MVVM.ViewModels;
using System.ComponentModel.Composition;
using VVVV.Core.Logging;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VColor;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetColor", Category = "CMiX_VVVV")]
    public class GetColor : IPluginEvaluate
    {
        [Import()]
        public ILogger FLogger;

        [Input("Entity")]
        public IDiffSpread<Entity> FComponentIn;

        [Output("Color")]
        public ISpread<RGBAColor> FColor;
        public void Evaluate(int SpreadMax)
        {
            FColor.SliceCount = FComponentIn.SliceCount;
            if (FComponentIn.SliceCount > 0)
            {
                for (int i = 0; i < FComponentIn.SliceCount; i++)
                {
                    if (FComponentIn[i] != null)
                    {
                        var c = FComponentIn[i].Coloration.ColorSelector.ColorPicker.SelectedColor;
                        FColor[i] = System.Drawing.Color.FromArgb((int)c.A, (int)c.R, (int)c.G, (int)c.B);
                    }
                    else
                        FColor.SliceCount = 0;
                }
            }
        }
    }
}