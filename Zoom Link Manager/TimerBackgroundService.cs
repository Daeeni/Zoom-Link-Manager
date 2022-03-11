using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zoom_Link_Manager
{
    public class TimerBackgroundService
    {
        public static System.Timers.Timer SetTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer(300000);
            timer.AutoReset = true;
            timer.Enabled = true;
            return timer;
        }
    }
}
