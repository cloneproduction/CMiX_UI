using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels;
using VVVV.PluginInterfaces.V2;

namespace VVVV.Nodes.Modifiers
{
    [PluginInfo(Name = "GetModifier", Category = "CMiX_VVVV")]
    public class GetModifier : IPluginEvaluate
    {
        //[Input("Instancer")]
        //public IDiffSpread<IModifier> FModifierIn;

        [Output("X")]
        public ISpread<AnimParameter> X;

        [Output("Y")]
        public ISpread<AnimParameter> Y;

        [Output("Z")]
        public ISpread<AnimParameter> Z;

        public void Evaluate(int SpreadMax)
        {
            //X.SliceCount = FModifierIn.SliceCount;
            //Y.SliceCount = FModifierIn.SliceCount;
            //Z.SliceCount = FModifierIn.SliceCount;

            //if (FModifierIn.SliceCount > 0)
            //{
            //    for (int i = 0; i < FModifierIn.SliceCount; i++)
            //    {
            //        if (FModifierIn[i] != null)
            //        {
            //            X[i] = FModifierIn[i].X;
            //            Y[i] = FModifierIn[i].Y;
            //            Z[i] = FModifierIn[i].Z;
            //        }
            //        else
            //        {
            //            X.SliceCount = 0;
            //            Y.SliceCount = 0;
            //            Z.SliceCount = 0;
            //        }
            //    }
            //}
        }
    }
}
