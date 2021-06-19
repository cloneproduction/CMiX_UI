using System;

namespace CMiX.Core.Resources
{
    public static class RandomNumbers
    {
        private static Random random = new Random();
        //=-------------------------------------------------------------------
        // double between min and the max number
        public static double RandomDouble(double min, double max)
        {
            return (random.NextDouble() * (max - min)) + min;
        }
        //=----------------------------------
        // double between 0 and the max number
        public static double RandomDouble(int max)
        {
            return (random.NextDouble() * max);
        }
        //=-------------------------------------------------------------------
        // int between the min and the max number
        public static int RandomInt(int min, int max)
        {
            return random.Next(min, max + 1);
        }
        //=----------------------------------
        // int between 0 and the max number
        public static int RandomInt(int max)
        {
            return random.Next(max + 1);
        }
        //=-------------------------------------------------------------------
    }
}
