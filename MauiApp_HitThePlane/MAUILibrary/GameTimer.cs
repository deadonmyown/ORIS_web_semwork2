using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MAUILibrary
{
    public class GameTimer
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern void NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);

        [DllImport("ntdll.dll")]
        private static extern bool NtDelayExecution(bool Alertable, ref long DelayInterval);

        private Thread _thread;
        public bool Running { get; private set; }

        public void Start(int intervalMs, Action callback)
        {
            var res = (uint)(intervalMs * 10000);
            NtSetTimerResolution(res, true, ref res);

            _thread = new Thread(() =>
            {
                Running = true;

                while (Running)
                {
                    var interval = -intervalMs * 10000L;
                    NtDelayExecution(false, ref interval);
                    callback();
                }
            })
            { Priority = ThreadPriority.Highest };

            _thread.Start();
        }

        public void Stop()
        {
            Running = false;
        }
    }
}