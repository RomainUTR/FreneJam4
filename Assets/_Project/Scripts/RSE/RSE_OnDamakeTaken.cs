using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RSE_OnDamakeTaken", menuName = "Scriptable Objects/RSE_OnDamakeTaken")]
public class RSE_OnDamakeTaken : ScriptableObject
{
    public UnityAction<float> OnEventRaised;

    public void RaiseEvent(float amount)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(amount);
        }
    }
}
