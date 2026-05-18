using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RSE_OnHealPlayer", menuName = "Scriptable Objects/RSE_OnHealPlayer")]
public class RSE_OnHealPlayer : ScriptableObject
{
    public event UnityAction<int> OnEventRaised;
    
    public void RaiseEvent(int value)
    {
        OnEventRaised(value);
    }
}
