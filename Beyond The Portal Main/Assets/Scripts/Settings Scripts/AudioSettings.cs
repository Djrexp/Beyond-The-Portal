using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource previewAudioSource;

    [Header("Volume Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private const string MASTER_VOL = "masterVolume";
    private const string MUSIC_VOL = "musicVolume";
    private const string SFX_VOL = "sfxVolume";

    private void Start(){
        masterSlider.onValueChanged.AddListener(value => SetVolume(MASTER_VOL, value));
        musicSlider.onValueChanged.AddListener(value => SetVolume(MUSIC_VOL, value));
        sfxSlider.onValueChanged.AddListener(value => SetVolume(SFX_VOL, value));

        masterSlider.onValueChanged.AddListener(_ => PlayPreviewSound());
    }

    private void SetVolume(string parameter, float value){
        audioMixer.SetFloat(parameter, Mathf.Log10(value) * 20);
    }

    private void PlayPreviewSound(){
        if (previewAudioSource != null && !previewAudioSource.isPlaying){
            previewAudioSource.Play();
        }
    }

    public void ResetAudioToDefault(){
        masterSlider.value = 0.5f;
        musicSlider.value = 0.5f;
        sfxSlider.value = 0.5f;
    }
}