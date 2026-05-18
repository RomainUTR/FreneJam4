using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RSE_OnPlayerDeath", menuName = "Scriptable Objects/RSE_OnPlayerDeath")]
public class RSE_OnPlayerDeath : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke();
        }
    }
}
