using CMiX.Core.Presentation.ViewModels;

namespace CMiX.Core.Interfaces
{
    public interface IModifier
    {
        string Name { get; set; }

        AnimParameter X { get; set; }
        AnimParameter Y { get; set; }
        AnimParameter Z { get; set; }
    }
}
