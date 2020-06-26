using CMiX.MVVM.ViewModels;
using VVVV.PluginInterfaces.V2;

namespace CMiX.Nodes
{
    [PluginInfo(Name = "TranslateModifier", Category = "CMiX_VVVV")]
    public class GetTranslateModifier : IPluginEvaluate
    {
        [Input("Instancer")]
        public ISpread<Instancer> FInstancerIn;

        //[Output("Translate")]
        //public ISpread<AnimParameter> FTranslate;

        [Output("TranslateInfluence")]
        public ISpread<double> TranslateInfluence;

        [Output("TranslateMode")]
        public ISpread<string> TranslateMode;



        [Output("TranslateXInfluence")]
        public ISpread<double> TranslateXInfluence;

        [Output("TranslateXMode")]
        public ISpread<string> TranslateXMode;


        [Output("TranslateYInfluence")]
        public ISpread<double> TranslateYInfluence;

        [Output("TranslateYMode")]
        public ISpread<string> TranslateYMode;


        [Output("TranslateZInfluence")]
        public ISpread<double> TranslateZInfluence;

        [Output("TranslateZMode")]
        public ISpread<string> TranslateZMode;

        public void Evaluate(int SpreadMax)
        {
            TranslateInfluence.SliceCount = FInstancerIn.SliceCount;
            TranslateMode.SliceCount = FInstancerIn.SliceCount;

            TranslateXInfluence.SliceCount = FInstancerIn.SliceCount;
            TranslateXMode.SliceCount = FInstancerIn.SliceCount;

            TranslateYInfluence.SliceCount = FInstancerIn.SliceCount;
            TranslateYMode.SliceCount = FInstancerIn.SliceCount;

            TranslateZInfluence.SliceCount = FInstancerIn.SliceCount;
            TranslateZMode.SliceCount = FInstancerIn.SliceCount;

            if (FInstancerIn.SliceCount > 0)
            {
                for (int i = 0; i < FInstancerIn.SliceCount; i++)
                {
                    if (FInstancerIn[i] != null)
                    {
                        TranslateInfluence[i] = FInstancerIn[i].TranslateModifier.Translate.Influence.Amount;
                        TranslateMode[i] = FInstancerIn[i].TranslateModifier.Translate.Mode.ToString();

                        TranslateXInfluence[i] = FInstancerIn[i].TranslateModifier.TranslateX.Influence.Amount;
                        TranslateXMode[i] = FInstancerIn[i].TranslateModifier.TranslateX.Mode.ToString();

                        TranslateYInfluence[i] = FInstancerIn[i].TranslateModifier.TranslateY.Influence.Amount;
                        TranslateYMode[i] = FInstancerIn[i].TranslateModifier.TranslateY.Mode.ToString();

                        TranslateZInfluence[i] = FInstancerIn[i].TranslateModifier.TranslateZ.Influence.Amount;
                        TranslateZMode[i] = FInstancerIn[i].TranslateModifier.TranslateZ.Mode.ToString();
                    }
                    else
                    {
                        TranslateInfluence.SliceCount = 0;
                        TranslateXInfluence.SliceCount = 0;
                        TranslateYInfluence.SliceCount = 0;
                        TranslateZInfluence.SliceCount = 0;
                    }
                }
            }
        }
    }
}
