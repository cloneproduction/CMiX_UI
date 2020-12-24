using CMiX.MVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public class RandomXYZ : Sender
    {
        public RandomXYZ(string name, Sender parentSender, MasterBeat masterBeat) : base (name, parentSender)
        {
            Random = new Random();
            BeatModifier = new BeatModifier(nameof(BeatModifier), this, masterBeat);
        }

        public BeatModifier BeatModifier { get; set; }
        private Random Random { get; set; }
        public Range Range { get; set; }
        public Counter Seed { get; set; }

        public override void Receive(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
