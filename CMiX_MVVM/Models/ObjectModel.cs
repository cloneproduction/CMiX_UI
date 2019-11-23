using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Models
{
    public class ObjectModel : Model
    {
        public ObjectModel()
        {
            Enable = true;
            BeatModifierModel = new BeatModifierModel();
            GeometryModel = new GeometryModel();
            TextureModel = new TextureModel();
            ColorationModel = new ColorationModel();
        }

        public ObjectModel(string messageAddress)
            : this()
        {
            MessageAddress = messageAddress;
        }

        public bool Enable { get; set; }
        public string Name { get; set; }

        public BeatModifierModel BeatModifierModel { get; set; }
        public GeometryModel GeometryModel { get; set; }
        public TextureModel TextureModel { get; set; }
        public ColorationModel ColorationModel { get; set; }
    }
}
