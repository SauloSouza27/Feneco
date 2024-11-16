using System;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    
    [SerializeField] private AudioSource jumpSFX;
    [SerializeField] private AudioSource dashSFX;
    [SerializeField] private AudioSource openInventorySFX;
    [SerializeField] private AudioSource equipSwordSFX;
    [SerializeField] private AudioSource equipHelmetSFX;
    [SerializeField] private AudioSource equipArmorSFX;
    [SerializeField] private AudioSource enemyDeathSFX;
    [SerializeField] private AudioSource acceptQuestSFX;
    [SerializeField] private AudioSource finishQuestSFX;


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
        }
    }

    public void PlayJumpSFX()
    {
        if (jumpSFX != null)
        {
            jumpSFX.Play();
        }
        else
        {
            Debug.LogWarning("Jump SFX not assigned!");
        }
    }

    public void PlayDashSFX()
    {
        if (dashSFX != null)
        {
            dashSFX.Play();
        }
        else
        {
            Debug.LogWarning("Hit SFX not assigned!");
        }
    }
    public void PlayOpenInventorySFX()
    {
        if (openInventorySFX != null)
        {
            openInventorySFX.Play();
        }
        else
        {
            Debug.LogWarning("Hit SFX not assigned!");
        }
    }
    
    public void PlayEquipSwordSFX()
    {
        if (equipSwordSFX != null)
        {
            equipSwordSFX.Play();
        }
        else
        {
            Debug.LogWarning("Hit SFX not assigned!");
        }
    }
    
    public void PlayEquipHelmetSFX()
    {
        if (equipHelmetSFX != null)
        {
            equipHelmetSFX.Play();
        }
        else
        {
            Debug.LogWarning("Hit SFX not assigned!");
        }
    }
    
    public void PlayEquipArmorSFX()
    {
        if (equipArmorSFX != null)
        {
            equipArmorSFX.Play();
        }
        else
        {
            Debug.LogWarning("Hit SFX not assigned!");
        }
    }
    
    public void PlayEnemyDeathSFX()
    {
        if (enemyDeathSFX != null)
        {
            enemyDeathSFX.Play();
        }
        else
        {
            Debug.LogWarning("Hit SFX not assigned!");
        }
    }
    
    public void PlayAcceptQuestSFX()
    {
        if (acceptQuestSFX != null)
        {
            acceptQuestSFX.Play();
        }
        else
        {
            Debug.LogWarning("Hit SFX not assigned!");
        }
    }
    
    public void PlayFinishQuestSFX()
    {
        if (finishQuestSFX != null)
        {
            finishQuestSFX.Play();
        }
        else
        {
            Debug.LogWarning("Hit SFX not assigned!");
        }
    }
    
}