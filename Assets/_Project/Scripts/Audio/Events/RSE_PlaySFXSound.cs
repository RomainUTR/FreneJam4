using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RSO_PlaySFXSound", menuName = "Events/Audio/RSE_PlaySFXSound")]
public class RSE_PlaySFXSound : ScriptableObject
{
    public Action<SoundData, Vector3> OnEventRaised;

    public void RaiseEvent(SoundData sound, Vector3 position)
    {
        OnEventRaised?.Invoke(sound, position);
    }
}
