using UnityEngine;

public abstract class UniqueEffectBase<TUniqueEvent> : MonoBehaviour where TUniqueEvent : IEvent
{
    private bool m_isRegistered = false;

    public void OnRegister()
    {
        if (m_isRegistered) return;

        EventBus.Register<TUniqueEvent>(OnEventRaised);
        m_isRegistered = true;
    }

    public void OnUnRegister()
    {
        if (!m_isRegistered) return;

        EventBus.UnRegister<TUniqueEvent>(OnEventRaised);
        m_isRegistered = false;
    }

    protected abstract void OnEventRaised(TUniqueEvent eventData);

    protected virtual void OnDisable()
    {
        OnUnRegister();
    }
}
