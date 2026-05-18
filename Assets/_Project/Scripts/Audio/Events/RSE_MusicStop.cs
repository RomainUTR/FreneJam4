using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_MusicStop", menuName = "Events/Audio/RSE_MusicStop")]
public class RSE_MusicStop : ScriptableObject
{
    public Action OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}
