using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VMath;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "GetInstancer", Category = "CMiX_VVVV")]
    public class GetInstancer : IPluginEvaluate
    {
        [Input("Geometry")]
        public IDiffSpread<Geometry> FGeometryIn;

        [Output("Instancer")]
        public ISpread<Instancer> FInstancer;

        [Output("TransformModifier")]
        public ISpread<ISpread<Matrix4x4>> FTransformModifier;

        public void Evaluate(int SpreadMax)
        {
            FInstancer.SliceCount = FGeometryIn.SliceCount;

            if (FGeometryIn.SliceCount > 0)
            {
                for (int i = 0; i < FGeometryIn.SliceCount; i++)
                {
                    if (FGeometryIn[i] != null)
                    {
                        FInstancer[i] = FGeometryIn[i].Instancer;
                        FTransformModifier[i].SliceCount = FGeometryIn[i].Instancer.TransformModifiers.Count;

                        for (int j = 0; j < FGeometryIn[j].Instancer.TransformModifiers.Count; j++)
                        {
                            //FTransformModifier[i][j] = VMath.Transform(FGeometryIn[j].Instancer.TransformModifiers[j].Location, FGeometryIn[j].Instancer.TransformModifiers[j].Scale, FGeometryIn[j].Instancer.TransformModifiers[j].Rotation);
                        }


                    }
                    else
                        FInstancer.SliceCount = 0;
                }
            }
        }
    }
}