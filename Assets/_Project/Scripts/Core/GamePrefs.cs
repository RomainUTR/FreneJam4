using System.Collections.Generic;
using UnityEngine;

public enum AudioChannel
{
    Master,
    Music,
    SFX,
    UI
}

public static class GamePrefs
{
    public static readonly Dictionary<AudioChannel, string> VolumeKeys = new()
    {
        { AudioChannel.Master, "VolumMasterParam" },
        { AudioChannel.Music,  "VolumMusicParam" },
        { AudioChannel.SFX,    "VolumSFXParam" },
        { AudioChannel.UI,     "VolumUIParam" }
    };

    public static float GetVolume(AudioChannel channel)
    {
        return PlayerPrefs.GetFloat(VolumeKeys[channel], 1.0f);
    }

    public static void SetVolume(AudioChannel channel, float volume)
    {
        PlayerPrefs.SetFloat(VolumeKeys[channel], Mathf.Clamp01(volume));
        PlayerPrefs.Save();
    }

    private static void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    private static bool GetBool(string key, bool defaultValue)
    {
        return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }
}