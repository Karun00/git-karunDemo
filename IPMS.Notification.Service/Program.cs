using System;
using System.ServiceProcess;

namespace IPMSNotificationService
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                MessageQueueProcessor queueProcessor = new MessageQueueProcessor();
                queueProcessor.ProcessMessage(args[0]);

               // Scheduler pcsCargoServ = new Scheduler();
                //pcsCargoServ.Start_Notification_Service();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new Scheduler(),
                };

                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
