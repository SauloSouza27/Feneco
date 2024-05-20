using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loot : MonoBehaviour
{
    [Header("Called on Trigger")]
    [SerializeField] private GameObject callbackObject = null;
    [SerializeField] private string method;

    private new BoxCollider collider;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Item item;

    private void Awake()
    {
        collider = transform.GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool canAdd = InventoryManager.instance.AddItem(item);
            if (canAdd)
            {
                StartCoroutine(MoveAndCollect(other.transform));
            }

            if (callbackObject != null && method != "")
            {
                callbackObject.SendMessage(method);
            }
        }
    }

    private IEnumerator MoveAndCollect(Transform target)
    {
        Destroy(collider);

        while (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            yield return 0;
        }
        Destroy(gameObject);
    }
}
