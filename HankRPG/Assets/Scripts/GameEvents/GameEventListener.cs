using UnityEngine;
using UnityEngine.Events;

public class GameEventListener<T> : MonoBehaviour, IGameEventListener<T>
{
    [SerializeField]
    private GameEvent<T> _gameEvent;

    [SerializeField]
    private UnityEvent _unityEvent;

    void OnEnable() => _gameEvent.Register(this);

    private void OnDisable() => _gameEvent.Deregister(this);

    public virtual void OnEventRaised(T item) => _unityEvent.Invoke();
}

public interface IGameEventListener<T>
{
    void OnEventRaised(T item);
}
