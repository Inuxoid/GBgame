using System;
using System.Collections.Generic;

public static class MainThreadDispatcher
{
    private static readonly Queue<Action> actions = new Queue<Action>();

    public static void Enqueue(Action action)
    {
        lock (actions)
        {
            actions.Enqueue(action);
        }
    }

    public static void Update()
    {
        lock (actions)
        {
            while (actions.Count > 0)
            {
                actions.Dequeue().Invoke();
            }
        }
    }
}