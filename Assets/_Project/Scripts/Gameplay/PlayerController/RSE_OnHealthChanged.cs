using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RSE_OnDamakeTaken", menuName = "Scriptable Objects/RSE_OnHealthChanged")]
public class RSE_OnHealthChanged : ScriptableObject
{
    public UnityAction<int> OnEventRaised;

    public void RaiseEvent(int value)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(value);
        }
    }
}
