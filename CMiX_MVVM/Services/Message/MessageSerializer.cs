using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceras;

namespace CMiX.MVVM.Services
{
    public static class MessageSerializer
    {
        private static CerasSerializer _serializer;

        public static CerasSerializer Serializer => _serializer ?? (_serializer = new CerasSerializer());
    }
}
