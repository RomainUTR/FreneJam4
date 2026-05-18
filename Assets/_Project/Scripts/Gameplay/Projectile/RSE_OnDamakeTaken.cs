using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RSE_OnDamakeTaken", menuName = "Scriptable Objects/RSE_OnDamakeTaken")]
public class RSE_OnDamakeTaken : ScriptableObject
{
    public UnityAction<int> OnEventRaised;

    public void RaiseEvent(int amount)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(amount);
        }
    }
}
