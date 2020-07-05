using CMiX.MVVM.ViewModels;
using VVVV.PluginInterfaces.V2;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "TranslateModifier", Category = "CMiX_VVVV")]
    public class GetTranslateModifier : IPluginEvaluate
    {
        [Input("Instancer")]
        public ISpread<Instancer> FInstancerIn;

        [Output("Translate")]
        public ISpread<AnimParameter> Translate;

        [Output("TranslateX")]
        public ISpread<AnimParameter> TranslateX;

        [Output("TranslateY")]
        public ISpread<AnimParameter> TranslateY;

        [Output("TranslateZ")]
        public ISpread<AnimParameter> TranslateZ;

        public void Evaluate(int SpreadMax)
        {
            Translate.SliceCount = FInstancerIn.SliceCount;
            TranslateX.SliceCount = FInstancerIn.SliceCount;
            TranslateY.SliceCount = FInstancerIn.SliceCount;
            TranslateZ.SliceCount = FInstancerIn.SliceCount;

            if (FInstancerIn.SliceCount > 0)
            {
                for (int i = 0; i < FInstancerIn.SliceCount; i++)
                {
                    if (FInstancerIn[i] != null)
                    {
                        Translate[i] = FInstancerIn[i].TranslateModifier.Translate;
                        TranslateX[i] = FInstancerIn[i].TranslateModifier.TranslateX;
                        TranslateY[i] = FInstancerIn[i].TranslateModifier.TranslateY;
                        TranslateZ[i] = FInstancerIn[i].TranslateModifier.TranslateZ;
                    }
                    else
                    {
                        Translate.SliceCount = 0;
                        TranslateX.SliceCount = 0;
                        TranslateY.SliceCount = 0;
                        TranslateZ.SliceCount = 0;
                    }
                }
            }
        }
    }
}