using CMiX.MVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public class Message
    {
        public Message(string address, byte[] data)
        {
            Address = address;
            Data = data;
        }

        public string Address { get; set; }

        public byte[] Data { get; set; }
    }
}
