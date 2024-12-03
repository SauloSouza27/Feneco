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

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        collider = transform.GetComponent<BoxCollider>();
        meshRenderer = transform.GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool canAdd = InventoryManager.instance.AddItem(item);

            if (callbackObject != null && method != "")
            {
                callbackObject.SendMessage(method);
                GameController.instance.SetQuestHint();
                callbackObject.GetComponent<NPC>().receivedItem = item as ObjectiveItem;
                Debug.Log(callbackObject.GetComponent<NPC>().receivedItem);
            }

            if (canAdd)
            {
                StartCoroutine(MoveAndCollect(other.transform));
            }

            
        }
    }

    private IEnumerator MoveAndCollect(Transform target)
    {
        Destroy(collider);

        while (Vector3.Distance(transform.position, target.position) > 0.01f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            yield return null; 
        }
        Destroy(gameObject);
    }

    public void TurnOnObjective()
    {
        collider.enabled = true;
        meshRenderer.enabled = true;
    }
}
