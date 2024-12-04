using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private MeshCollider deathZone;

    private Transform respawnPoint;

    [SerializeField] private float timeToRespawn = 0.6f;

    private void Start()
    {
        deathZone = this.GetComponent<MeshCollider>();
        respawnPoint = deathZone.transform.GetChild(0).gameObject.GetComponent<Transform>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SFXManager.instance.PlaySFX(SFXManager.SFXType.FallOnWater);
        }
        StartCoroutine(RespawnCheckpoint(other));
    }

    private IEnumerator RespawnCheckpoint(Collider other)
    {
        yield return new WaitForSeconds(timeToRespawn);
        other.gameObject.transform.position = respawnPoint.position;
    }

    private void OnDrawGizmos()
    {
        if (!Application.IsPlaying(this))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(0).position, 0.5f);
        }
    }
}
