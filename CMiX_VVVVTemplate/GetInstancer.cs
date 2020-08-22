using CMiX.MVVM.Interfaces;
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

        [Output("TranslateModifier")]
        public ISpread<IModifier> TranslateModifier;

        [Output("ScaleModifier")]
        public ISpread<IModifier> ScaleModifier;

        [Output("RotationModifier")]
        public ISpread<IModifier> RotationModifier;

        public void Evaluate(int SpreadMax)
        {
            FInstancer.SliceCount = FGeometryIn.SliceCount;
            TranslateModifier.SliceCount = FGeometryIn.SliceCount;
            ScaleModifier.SliceCount = FGeometryIn.SliceCount;
            RotationModifier.SliceCount = FGeometryIn.SliceCount;

            if (FGeometryIn.SliceCount > 0)
            {
                for (int i = 0; i < FGeometryIn.SliceCount; i++)
                {
                    if (FGeometryIn[i] != null)
                    {
                        FInstancer[i] = FGeometryIn[i].Instancer;
                        TranslateModifier[i] = FGeometryIn[i].Instancer.TranslateModifier;
                        ScaleModifier[i] = FGeometryIn[i].Instancer.ScaleModifier;
                        RotationModifier[i] = FGeometryIn[i].Instancer.RotationModifier;
                    }
                    else
                    {
                        FInstancer.SliceCount = 0;
                        TranslateModifier.SliceCount = 0;
                        ScaleModifier.SliceCount = 0;
                        RotationModifier.SliceCount = 0;
                    }
                }
            }
        }
    }
}