using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Core.Models.Beat
{
    public class ResyncModel : Model
    {
        public ResyncModel()
        {
            this.ID = Guid.NewGuid();
        }
    }
}
