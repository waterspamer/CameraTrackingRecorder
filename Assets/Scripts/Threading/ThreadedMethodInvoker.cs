using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Threading
{
    public static class ThreadedMethodInvoker
    {
        public static Task StartSynchronizedRoutine(Action invokedAction, int millisecondsDelay,
            CancellationTokenSource source)
        {
            return new Task(async () =>
            {
                while (!source.IsCancellationRequested)
                {
                    try
                    {
                        invokedAction?.Invoke();
                        await Task.Delay(new TimeSpan(0, 0, 0, 0, millisecondsDelay));
                    }
                    catch(Exception exception)
                    {
                        Debug.LogError(exception.Message);
                        throw;
                    }
                }
                Debug.Log($"IsCancellationRequested {source.IsCancellationRequested}");
            });
        }
    }
}
