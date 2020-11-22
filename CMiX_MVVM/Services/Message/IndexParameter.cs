using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.Services
{
    [Serializable]
    public class IndexParameter
    {
        public IndexParameter()
        {

        }
        public IndexParameter(int index)
        {
            Index = index;
        }

        public int Index { get; set; }
    }
}
