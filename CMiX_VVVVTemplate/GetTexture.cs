using CMiX.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVVV.PluginInterfaces.V2;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetTexture", Category = "CMiX_VVVV")]
    public class GetTexture : IPluginEvaluate
    {
        [Input("Entity")]
        public IDiffSpread<Entity> FComponentIn;

        [Output("Texture")]
        public ISpread<Texture> FTexture;

        public void Evaluate(int SpreadMax)
        {
            if (FComponentIn.IsChanged)
            {
                if (FComponentIn != null)
                {
                    if (FComponentIn.SliceCount > 0)
                    {
                        FTexture.SliceCount = FComponentIn.SliceCount;

                        for (int i = 0; i < FComponentIn.SliceCount; i++)
                        {
                            FTexture[i] = FComponentIn[i].Texture;
                        }
                    }
                }
            }
        }
    }
}
