using System.Collections.Generic;
using UnityEngine;

public class GameEvent<T> : ScriptableObject
{
    private readonly HashSet<IGameEventListener<T>> _listeners = new HashSet<IGameEventListener<T>>();

    public void RaiseEvent(T item)
    {
        foreach (var globalEventListener in _listeners)
            globalEventListener.OnEventRaised(item);
    }

    public void Register(IGameEventListener<T> eventListener) => _listeners.Add(eventListener);

    public void Deregister(IGameEventListener<T> eventListener) => _listeners.Remove(eventListener);
}