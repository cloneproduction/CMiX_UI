using CMiX.MVVM.ViewModels;
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
            FTexture.SliceCount = FComponentIn.SliceCount;
            if (FComponentIn.IsChanged)
            {
                if (FComponentIn.SliceCount > 0)
                {
                    for (int i = 0; i < FComponentIn.SliceCount; i++)
                    {
                        if (FComponentIn[i] != null)
                            FTexture[i] = FComponentIn[i].Texture;
                        else
                            FTexture.SliceCount = 0;
                    }
                }
            }
        }
    }
}