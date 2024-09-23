using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPNotification.Console.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Start ar");

            SAPScheduler sch = new SAPScheduler();
            sch.OnStart();

            System.Console.WriteLine("Press Any Kwy to stop service");
            System.Console.ReadLine();

            sch.OnStop();

            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[] 
            //{ 
            //    new Scheduler() 
            //};
            //ServiceBase.Run(ServicesToRun);

        }
    }
}
