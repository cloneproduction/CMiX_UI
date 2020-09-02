using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.Interfaces
{
    public interface IModifier
    {
        string Name { get; set; }
       // AnimParameter Uniform { get; set; }
        AnimParameter X { get; set; }
        AnimParameter Y { get; set; }
        AnimParameter Z { get; set; }
    }
}
