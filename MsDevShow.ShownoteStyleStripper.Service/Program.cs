using System;
using System.ServiceProcess;

namespace MsDevShow.ShownoteStyleStripper.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                var war = new WatchAndRip();
                new System.Threading.AutoResetEvent(false).WaitOne();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new ShownoteStyleStripperService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
