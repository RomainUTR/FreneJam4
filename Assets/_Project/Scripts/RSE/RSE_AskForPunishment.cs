using UnityEngine;
using System;

[CreateAssetMenu(fileName = "RSE_AskForPunishment", menuName = "Events/RSE_AskForPunishment")]
public class RSE_AskForPunishment : ScriptableObject
{
    public event Action OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}