using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider musicSlider;

    private void Start()
    {
        // Initialize sliders with current volume levels
        masterSlider.value = PlayerPrefs.GetFloat("Master", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("Music", 1f);

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);

        // Apply initial volume levels
        SetMasterVolume(masterSlider.value);
        SetSFXVolume(sfxSlider.value);
        SetMusicVolume(musicSlider.value);
    }

    public void SetMasterVolume(float volume)
    {
        Debug.Log("Setting Master Volume: " + volume);
        SetVolume("Master", volume);
        PlayerPrefs.SetFloat("Master", volume);
    }

    public void SetSFXVolume(float volume)
    {
        Debug.Log("Setting SFX Volume: " + volume);
        SetVolume("SFX", volume);
        PlayerPrefs.SetFloat("SFX", volume);
    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log("Setting Music Volume: " + volume);
        SetVolume("Music", volume);
        PlayerPrefs.SetFloat("Music", volume);
    }

    private void SetVolume(string parameterName, float volume)
    {
        if (volume == 0)
        {
            audioMixer.SetFloat(parameterName, -80f); // Set to a very low value to simulate silence
        }
        else
        {
            audioMixer.SetFloat(parameterName, Mathf.Log10(volume) * 20);
        }
    }
}