using CMiX.MVVM.ViewModels;
using VVVV.PluginInterfaces.V2;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetInstancer", Category = "CMiX_VVVV")]
    public class GetInstancer : IPluginEvaluate
    {
        [Input("Geometry")]
        public ISpread<Geometry> FGeometryIn;

        [Output("Instancer")]
        public ISpread<Instancer> FInstancer;

        public void Evaluate(int SpreadMax)
        {
            FInstancer.SliceCount = FGeometryIn.SliceCount;
            if (FGeometryIn.SliceCount > 0)
            {
                for (int i = 0; i < FGeometryIn.SliceCount; i++)
                {
                    if (FGeometryIn[i] != null)
                        FInstancer[i] = FGeometryIn[i].Instancer;
                    else
                        FInstancer.SliceCount = 0;
                }
            }
        }
    }
}