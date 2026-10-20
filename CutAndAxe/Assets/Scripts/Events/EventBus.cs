using System;
using System.Collections.Generic;

public interface IEvent { }

public static class EventBus
{
    private static readonly Dictionary<Type, Delegate> _registers = new Dictionary<Type, Delegate>();

    public static void Register<T>(Action<T> action) where T : IEvent
    {
        var type = typeof(T);
        if (_registers.ContainsKey(type))
        {
            _registers[type] = Delegate.Combine(_registers[type], action);
        }
        else
        {
            _registers[type] = action;
        }
    }

    public static void UnRegister<T>(Action<T> action) where T : IEvent
    {
        var type = typeof(T);
        if (_registers.ContainsKey(type))
        {
            var currentDel = Delegate.Remove(_registers[type], action);
            if (currentDel == null)
            {
                _registers.Remove(type);
            }
            else
            {
                _registers[type] = currentDel;
            }
        }
    }

    public static void Publish<T>(T eventData) where T : IEvent
    {
        var type = typeof(T);
        if (_registers.TryGetValue(type, out var del))
        {
            (del as Action<T>)?.Invoke(eventData);
        }
    }
}
