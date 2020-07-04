using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.Interfaces
{
    public interface IBeatModifier : IBeat
    {
        BeatModifier BeatModifier { get; set; }
    }
}
