using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class TranslateModel : Model
    {
        public TranslateModel()
        {
            TranslateX = new SliderModel();
            TranslateY = new SliderModel();
            TranslateZ = new SliderModel();
        }

        public TranslateModel(string messageaddress)
            : this()
        {
            MessageAddress = messageaddress;
            TranslateX = new SliderModel(messageaddress);
            TranslateY = new SliderModel(messageaddress);
            TranslateZ = new SliderModel(messageaddress);
        }

        public SliderModel TranslateX { get; set; }
        public SliderModel TranslateY { get; set; }
        public SliderModel TranslateZ { get; set; }
    }
}
