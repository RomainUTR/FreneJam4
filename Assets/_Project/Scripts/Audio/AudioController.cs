using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioMixer audioMixer;

    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider uiSlider;

    private void Start()
    {
        InitializeSlider(AudioChannel.Master, masterSlider);
        InitializeSlider(AudioChannel.Music, musicSlider);
        InitializeSlider(AudioChannel.SFX, sfxSlider);
        InitializeSlider(AudioChannel.UI, uiSlider);
    }

    private void InitializeSlider(AudioChannel channel, Slider slider)
    {
        if (slider == null) return;

        float savedVolumeLin = GamePrefs.GetVolume(channel);

        SetMixerVolume(channel, savedVolumeLin);

        slider.SetValueWithoutNotify(savedVolumeLin);

        slider.onValueChanged.AddListener((val) =>
        {
            SetMixerVolume(channel, val);
            GamePrefs.SetVolume(channel, val);
        });
    }

    public void SetMixerVolume(AudioChannel channel, float volumeLin)
    {
        float volumeInDB = volumeLin <= 0.0001f ? -80f : Mathf.Log10(volumeLin) * 20f;
        string mixerParameter = GamePrefs.VolumeKeys[channel];
        audioMixer.SetFloat(mixerParameter, volumeInDB);
    }
}