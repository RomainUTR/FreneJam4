using UnityEngine;
using System;

[CreateAssetMenu(fileName = "RSE_OnVictory", menuName = "Events/RSE_OnVictory")]
public class RSE_OnVictory : ScriptableObject
{
    public event Action OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}