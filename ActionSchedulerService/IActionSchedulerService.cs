using System;
using System.Collections.Generic;
using System.Text;

namespace ActionSchedulerService
{
    public interface IActionSchedulerService
    {
        void RegisterAction(Action action);
        void RegisterAction<T>(Action<T> action, T arg);
        void RegisterAction<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2);
        void RegisterAction<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3);
        //if you need you can add another register methods Action<T1, T2, T3, T4...

        void Start(int intervalMilliseconds);
        void Stop();
    }
}
