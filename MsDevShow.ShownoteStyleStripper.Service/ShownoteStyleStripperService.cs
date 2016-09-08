using System.ServiceProcess;
using System.Threading;

namespace MsDevShow.ShownoteStyleStripper.Service
{
    public partial class ShownoteStyleStripperService : ServiceBase
    {
        private WatchAndRip _war;

        private Thread _thread;

        private readonly ManualResetEvent _shutdownEvent = new ManualResetEvent(false);

        public ShownoteStyleStripperService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _war = new WatchAndRip(EventLog);

            //Create a thread to keep the app alive
            _thread = new Thread(WorkerThreadFunc)
            {
                Name = "MsDevShow.ShownoteStyleStripper.Service.WorkerThread",
                IsBackground = true
            };
            _thread.Start();
        }

        private void WorkerThreadFunc()
        {
            while (!_shutdownEvent.WaitOne(1000))
            {
                // Replace the Sleep() call with the work you need to do
                Thread.Sleep(1000);
            }
        }

        protected override void OnStop()
        {
            _war.Dispose();
            _war = null;

            // Kill the thread keeping the app going.
            _shutdownEvent.Set();
            if (!_thread.Join(3000))
            { // give the thread 3 seconds to stop
                _thread.Abort();
            }
        }
    }
}
