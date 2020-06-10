using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public static class BeatModelFactory
    {
        public static BeatModel GetModel(this Beat instance)
        {
            BeatModel beatModel = new BeatModel();
            beatModel.Period = instance.Period;
            return beatModel;
        }

        public static void SetViewModel(this Beat instance, BeatModel beatModel)
        {
            instance.Period = beatModel.Period;
        }
    }
}