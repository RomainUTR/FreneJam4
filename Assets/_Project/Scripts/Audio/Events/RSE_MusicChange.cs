using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RSE_MusicChange", menuName = "Events/Audio/RSE_MusicChange")]
public class RSE_MusicChange : ScriptableObject
{
    public Action<MusicTrack[]> OnEventRaised;

    public void RaiseEvent(MusicTrack[] tracks)
    {
        OnEventRaised?.Invoke(tracks);
    }
}
