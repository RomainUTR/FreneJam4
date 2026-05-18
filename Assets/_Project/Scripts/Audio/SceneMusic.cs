using UnityEngine;
using Sirenix.OdinInspector;

public class SceneMusic : MonoBehaviour
{
    [Title("Playlist & Mixage")]
    [ListDrawerSettings(ShowIndexLabels = false)]
    [SerializeField] private MusicTrack[] MusicLayers;

    [Title("Settings")]
    [SerializeField] private bool PlayOnStart = true;
    [SerializeField] private RSE_MusicChange MusicChange;

    private void Start()
    {
        if (PlayOnStart && MusicChange != null)
        {
            MusicChange?.RaiseEvent(MusicLayers);
        }
    }

    public void TriggerMusic()
    {
        if (MusicChange != null)
        {
            MusicChange?.RaiseEvent(MusicLayers);
        }
    }
}
