using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "RSE_OnComboFinished", menuName = "Scriptable Objects/RSE_OnComboFinished")]
public class RSE_OnComboFinished : ScriptableObject
{
    public event UnityAction<int> OnEventRaised;

    public void RaiseEvent(int amount)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(amount);
        }
    } 
}
