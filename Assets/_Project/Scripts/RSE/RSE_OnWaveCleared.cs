using UnityEngine;
using System;

[CreateAssetMenu(fileName = "RSE_OnWaveCleared", menuName = "Events/RSE_OnWaveCleared")]
public class RSE_OnWaveCleared : ScriptableObject
{
    public event Action OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}