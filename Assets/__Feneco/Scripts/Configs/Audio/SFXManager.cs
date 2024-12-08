using System;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public enum SFXType
    {
        Jump,
        Dash,
        OpenInventory,
        EquipSword,
        EquipHelmet,
        EquipArmor,
        EnemyDeath,
        AcceptQuest,
        FinishQuest,
        FallOnWater,
        EnemyGotHit
    }

    public enum MusicType
    {
        Default,
        Camp,
        Market,
        Oasis,
        Quests234,
        AlongTheWay,
        FinalQuest
    }

    [Serializable]
    public struct SFXClip
    {
        public SFXType type;
        public AudioSource audioSource;
    }

    [Serializable]
    public struct MusicClip
    {
        public MusicType type;
        public AudioClip clip;
    }

    [Header("SFX Settings")]
    [SerializeField] private List<SFXClip> sfxClips = new List<SFXClip>();

    [Header("Music Settings")]
    [SerializeField] private List<MusicClip> musicClips = new List<MusicClip>();
    [SerializeField] private AudioSource musicSource;

    private Dictionary<SFXType, AudioSource> sfxDictionary;
    private Dictionary<MusicType, AudioClip> musicDictionary;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize dictionaries
            sfxDictionary = new Dictionary<SFXType, AudioSource>();
            foreach (var sfxClip in sfxClips)
            {
                if (!sfxDictionary.ContainsKey(sfxClip.type))
                {
                    sfxDictionary.Add(sfxClip.type, sfxClip.audioSource);
                }
                else
                {
                    Debug.LogWarning($"Duplicate SFXType found: {sfxClip.type}");
                }
            }

            musicDictionary = new Dictionary<MusicType, AudioClip>();
            foreach (var musicClip in musicClips)
            {
                if (!musicDictionary.ContainsKey(musicClip.type))
                {
                    musicDictionary.Add(musicClip.type, musicClip.clip);
                }
                else
                {
                    Debug.LogWarning($"Duplicate MusicType found: {musicClip.type}");
                }
            }
        }
    }

    public void PlaySFX(SFXType sfxType)
    {
        if (sfxDictionary.TryGetValue(sfxType, out var audioSource) && audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"SFX not assigned or missing for type: {sfxType}");
        }
    }

    public void CrossfadeMusic(MusicType musicType, float fadeDuration = 1f)
    {
        if (musicDictionary.TryGetValue(musicType, out var newMusicClip) && newMusicClip != null)
        {
            if (musicSource.clip != newMusicClip)
            {
                StartCoroutine(FadeMusic(newMusicClip, fadeDuration));
            }
        }
        else
        {
            Debug.LogWarning($"Music not assigned or missing for type: {musicType}");
        }
    }

    private System.Collections.IEnumerator FadeMusic(AudioClip newMusicClip, float fadeDuration)
    {
        float startVolume = musicSource.volume;

        // Fade out
        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        // Switch music and fade in
        musicSource.clip = newMusicClip;
        musicSource.Play();

        while (musicSource.volume < startVolume)
        {
            musicSource.volume += startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
    }
}
