using System.Collections.Generic;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Dispatcher : MonoBehaviour
{
    public static void RunAsync(Action action) {
        ThreadPool.QueueUserWorkItem(o => action());
    }

    public static UnityEvent onThreadsRipped;
 
    public static void RunAsync(Action<object> action, object state) {
        ThreadPool.QueueUserWorkItem(o => action(o), state);
    }
 
    public static void RunOnMainThread(Action action) {
        lock(_backlog) {
            _backlog.Add(action);
            _queued = true;
        }
    }
 
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize() {
        if (_instance != null) return;
        _instance = new GameObject("Dispatcher").AddComponent<Dispatcher>();
        DontDestroyOnLoad(_instance.gameObject);
    }
 
    private void Update()
    {
        if (!_queued) return;
        lock(_backlog) {
            var tmp = _actions;
            _actions = _backlog;
            _backlog = tmp;
            _queued = false;
        }
 
        foreach(var action in _actions)
            action();
        
        _actions.Clear();
        onThreadsRipped?.Invoke();
    }

    private static Dispatcher _instance;
    static volatile bool _queued = false;
    static List<Action> _backlog = new List<Action>(8);
    static List<Action> _actions = new List<Action>(8);
}