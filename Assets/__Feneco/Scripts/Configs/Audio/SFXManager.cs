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
        FallOnWater
    }

    [Serializable]
    public struct SFXClip
    {
        public SFXType type;
        public AudioSource audioSource;
    }

    [SerializeField] private List<SFXClip> sfxClips = new List<SFXClip>();

    private Dictionary<SFXType, AudioSource> sfxDictionary;

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

            // Initialize the dictionary
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
}