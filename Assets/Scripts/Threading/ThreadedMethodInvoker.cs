using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using Accord;
using Antilatency.Alt.Tracking;
using Antilatency.Integration;
using UnityEditor.VersionControl;

public static class ThreadedMethodInvoker
{
    //public static CancellationToken source = new CancellationToken();
    public static void ThreadRoutine(CancellationTokenSource source, AltTrackingDirect altTrackingDirect)
    {
        var tsk = new System.Threading.Tasks.Task(() =>
        {
            while (!source.IsCancellationRequested)
            {
                altTrackingDirect.GetTrackingState(out State state);
                Debug.Log(state.pose.position.x);
                //Debug.Log(DateTime.Now.Millisecond);
                System.Threading.Tasks.Task.Delay(new TimeSpan(0, 0, 0, 0, 8), source.Token).Wait();
            }
        });
        tsk.Start();

    }
    
}
