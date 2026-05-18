using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RSE_OnDamakeTaken", menuName = "Scriptable Objects/RSE_OnHealthChanged")]
public class RSE_OnHealthChanged : ScriptableObject
{
    public UnityAction<float> OnEventRaised;

    public void RaiseEvent(float value)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(value);
        }
    }
}
