//using CMiX.Services;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.Models
{
    public class MasterBeatModel : IModel
    {

        public double Period { get; set; }
        public string MessageAddress { get; set; }
    }
}
