using CMiX.MVVM.ViewModels;
using VVVV.PluginInterfaces.V2;

namespace VVVV.Nodes.Modifiers
{
    [PluginInfo(Name = "GetUniformScale", Category = "CMiX_VVVV")]
    public class GetScaleModifier : IPluginEvaluate
    {
        [Input("Instancer")]
        public IDiffSpread<Instancer> FInstancerIn;

        [Output("Uniform")]
        public ISpread<AnimParameter> Uniform;

        public void Evaluate(int SpreadMax)
        {
            Uniform.SliceCount = FInstancerIn.SliceCount;

            if (FInstancerIn.SliceCount > 0)
            {
                for (int i = 0; i < FInstancerIn.SliceCount; i++)
                {
                    //if (FInstancerIn[i] != null)
                    //{
                    //    Uniform[i] = FInstancerIn[i].UniformScale;
                    //}
                    //else
                    //{
                    //    Uniform.SliceCount = 0;
                    //}
                }
            }
        }
    }
}