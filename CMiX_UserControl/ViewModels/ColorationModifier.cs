using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.ViewModels
{
    public enum ColorationModifier
    {
        [ShortCode("STD_CTR", "Steady Center")]
        STD_CTR,

        [ShortCode("FLA_RDM", "Flash Random")]
        FLA_RDM,

        [ShortCode("SLD_RDM", "Slide Random")]
        SLD_RDM,

        [ShortCode("SLD_LIN", "Slide Linear")]
        SLD_LIN,
    };
}
