using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_PlayUISound", menuName = "Events/Audio/RSE_PlayUISound")]
public class RSE_PlayUISound : ScriptableObject
{
    public Action<SoundData> OnEventRaised;

    public void RaiseEvent(SoundData data)
    {
        OnEventRaised?.Invoke(data);
    }
}
