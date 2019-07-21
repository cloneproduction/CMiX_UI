using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiXPlayer.Jobs
{
    public class MyRegistry : Registry
    {

        public MyRegistry ()
        {

        }

        public void Welcome()
        {
            Schedule(() => Console.Write("3, ")).WithName("[welcome]").ToRunEvery(10).Seconds();
        } 
    }
}
