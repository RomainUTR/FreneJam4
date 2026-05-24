using UnityEngine;
using System;

[CreateAssetMenu(fileName = "RSE_OnUpgradeFinished", menuName = "Events/RSE_OnUpgradeFinished")]
public class RSE_OnUpgradeFinished : ScriptableObject
{
    public event Action OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}