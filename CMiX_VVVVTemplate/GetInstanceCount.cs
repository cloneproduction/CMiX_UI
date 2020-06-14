using CMiX.MVVM.ViewModels;
using VVVV.PluginInterfaces.V2;


namespace VVVV.Nodes
{
    [PluginInfo(Name = "GetInstanceCount", Category = "CMiX_VVVV")]
    public class GetInstanceCount : IPluginEvaluate
    {
        [Input("Instancer")]
        public IDiffSpread<Instancer> FInstancerIn;

        [Output("Instance Count")]
        public ISpread<int> FInstanceCount;

        public void Evaluate(int SpreadMax)
        {

            if (FInstancerIn.SliceCount > 0)
            {
                FInstanceCount.SliceCount = FInstancerIn.SliceCount;

                for (int i = 0; i < FInstancerIn.SliceCount; i++)
                {
                    if (FInstancerIn[i] != null)
                        FInstanceCount[i] = FInstancerIn[i].Counter.Count;
                    else
                        FInstanceCount.SliceCount = 0;
                }
            }
        }
    }
}
