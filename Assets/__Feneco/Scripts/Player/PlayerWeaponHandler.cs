using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    public AudioSource swordWhooshSFX;
    public AudioClip[] swordWhooshSFXs; // Array to hold your sword sound effects
    [SerializeField] private float minSwordSoundPitch;
    [SerializeField] private float maxSwordSoundPitch;
    
    public AudioSource walkSound;
    public AudioClip[] walkSounds; // Array to hold your walk sound effects
    [SerializeField] private float minWalkSoundPitch;
    [SerializeField] private float maxWalkSoundPitch;
   
    
    
    public void PlayWalkSound()
    {
        if (walkSounds != null && walkSounds.Length > 0 && walkSound != null)
        {
            // Pick a random sound from the array
            int randomIndex = Random.Range(0, walkSounds.Length);
            AudioClip randomClip = walkSounds[randomIndex];

            // Assign and play the sound
            walkSound.clip = randomClip;
            walkSound.pitch = Random.Range(minWalkSoundPitch, maxWalkSoundPitch);
            walkSound.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or walk sounds not properly assigned!");
        }
    }
    
    public void PlaySwordSound()
    {
        if (swordWhooshSFXs != null && swordWhooshSFXs.Length > 0 && swordWhooshSFX != null)
        {
            // Pick a random sound from the array
            int randomIndex = Random.Range(0, swordWhooshSFXs.Length);
            AudioClip randomClip = swordWhooshSFXs[randomIndex];

            // Assign and play the sound
            swordWhooshSFX.clip = randomClip;
            swordWhooshSFX.pitch = Random.Range(minSwordSoundPitch, maxSwordSoundPitch);
            swordWhooshSFX.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or walk sounds not properly assigned!");
        }
    }
    
}
