using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;

    [Header("Soundtracks")]
    public AudioClip defaultMusic;
    public AudioClip forestMusic;
    public AudioClip caveMusic;
    
    
    public void CrossfadeMusic(AudioClip newMusic, float fadeDuration = 1f)
    {
        StartCoroutine(FadeMusic(newMusic, fadeDuration));
    }

    private IEnumerator FadeMusic(AudioClip newMusic, float fadeDuration)
    {
        float startVolume = musicSource.volume;

        // Fade out
        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        // Change music and fade in
        musicSource.clip = newMusic;
        musicSource.Play();

        while (musicSource.volume < startVolume)
        {
            musicSource.volume += startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
    }
}
