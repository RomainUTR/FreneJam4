using UnityEngine;
using System;

[CreateAssetMenu(fileName = "RSE_ToggleInputInversion", menuName = "Events/RSE_ToggleInputInversion")]
public class RSE_ToggleInputInversion : ScriptableObject
{
    public event Action OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}