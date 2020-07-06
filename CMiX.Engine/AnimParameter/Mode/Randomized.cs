using System;

namespace CMiX.Engine
{
    public class Randomized : Mode
    {
        public Randomized(Stopwatcher stopwatcher)
        {
            Stopwatcher = stopwatcher;
            Random = new Random();
            Stopwatcher.Change += new EventHandler(GenerateValue);
            UpdateValue = new Action(Update);
            RandomType = RandomType.JUMP;
        }

        public RandomType RandomType { get; set; }
        public Random Random { get; set; }

        public double newValue { get; set; }
        public double oldValue { get; set; }

        private void GenerateValue(object sender, EventArgs e)
        {
            oldValue = newValue;
            newValue = Random.NextDouble();
        }

        public void Update()
        {
            switch (this.RandomType)
            {
                case RandomType.JUMP:
                    {
                        ParameterValue = newValue;
                        break;
                    }
                case RandomType.LINEAR:
                    {
                        //ParameterValue = VMath.Lerp(oldValue, newValue, Stopwatcher.LFO);
                        break;
                    }
                default: break;
            }
        }
    }
}