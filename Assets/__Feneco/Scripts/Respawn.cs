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
        Debug.Log(deathZone.name + respawnPoint.name);
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(RespawnCheckPoint(other));
    }

    private IEnumerator RespawnCheckPoint(Collider other)
    {
        yield return new WaitForSeconds(timeToRespawn);
        other.gameObject.transform.position = respawnPoint.position;
    }
}