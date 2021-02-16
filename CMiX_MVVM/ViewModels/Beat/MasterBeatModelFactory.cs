using CMiX.MVVM.Models.Beat;

namespace CMiX.MVVM.ViewModels
{
    public static class MasterBeatModelFactory
    {
        public static MasterBeatModel GetModel(this MasterBeat instance)
        {
            MasterBeatModel model = new MasterBeatModel();
            model.Periods = instance.Periods;
            model.Multiplier = instance.Multiplier;
            return model;
        }

        public static void SetViewModel(this MasterBeat instance, MasterBeatModel model)
        {
            instance.Periods = model.Periods;
            instance.Multiplier = model.Multiplier;
        }
    }
}