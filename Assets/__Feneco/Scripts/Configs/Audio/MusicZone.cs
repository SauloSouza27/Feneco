using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicZone : MonoBehaviour
{
    public SFXManager.MusicType zoneMusic;
    public float crossfadeDuration = 1f;  
    private static int activeZones = 0;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SFXManager.instance.CrossfadeMusic(zoneMusic, crossfadeDuration);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activeZones--;
            if (activeZones <= 0) // No active zones left
            {
                SFXManager.instance.CrossfadeMusic(SFXManager.MusicType.Default, crossfadeDuration);
            }
        }
    }
}
