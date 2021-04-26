using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Antilatency.Alt.Tracking;
using Antilatency.Integration;
using UnityEngine;

namespace Threading
{
    public static class ThreadedMethodInvoker
    {
        public static Task StartSynchronizedRoutine(Action invokedAction, int millisecondsDelay, CancellationTokenSource source)
        {
            var tsk = new Task(() =>
            {
                while (!source.IsCancellationRequested)
                {
                    invokedAction?.Invoke();
                    Task.Delay(new TimeSpan(0, 0, 0, 0, millisecondsDelay)).Wait();
                }
            });
            tsk.Start();
            return tsk;
        }
        
        
        // public static Task StartSynchronizedRoutine(Action invokedAction, int millisecondsDelay, CancellationTokenSource source)
        // {
        //     var startTime = DateTime.Now;
        //     var tsk = new System.Threading.Tasks.Task(() =>
        //     {
        //         while (!source.IsCancellationRequested)
        //         {
        //             trackingDirect.GetTrackingState(out var state);
        //             invokedAction?.Invoke();
        //             System.Threading.Tasks.Task.Delay(new TimeSpan(0, 0, 0, 0, millisecondsDelay)).Wait();
        //         }
        //     });
        //     tsk.Start();
        //     return tsk;
        // }
        
        
    }
}
