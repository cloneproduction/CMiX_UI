using CMiX.MVVM.ViewModels;
using VVVV.PluginInterfaces.V2;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetInstancer", Category = "CMiX_VVVV")]
    public class GetInstancer : IPluginEvaluate
    {
        [Input("Geometry")]
        public IDiffSpread<Geometry> FGeometryIn;

        [Output("Instancer")]
        public ISpread<Instancer> FInstancer;

        public void Evaluate(int SpreadMax)
        {
            if (FGeometryIn.IsChanged)
            {
                if (FGeometryIn != null)
                {
                    if (FGeometryIn.SliceCount > 0)
                    {
                        FInstancer.SliceCount = FGeometryIn.SliceCount;

                        for (int i = 0; i < FGeometryIn.SliceCount; i++)
                        {
                            FInstancer[i] = FGeometryIn[i].Instancer;
                        }
                    }
                }
            }
        }
    }
}
