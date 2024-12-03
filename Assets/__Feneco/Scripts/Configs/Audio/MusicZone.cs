using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicZone : MonoBehaviour
{
    public SFXManager.MusicType zoneMusic; // Specify the music type for this zone
    public float crossfadeDuration = 1f;  // Duration for crossfade, if enabled

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SFXManager.instance.CrossfadeMusic(zoneMusic, crossfadeDuration);
        }
    }
}
