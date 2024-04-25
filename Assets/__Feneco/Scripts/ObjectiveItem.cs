using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveItem : MonoBehaviour
{
    [Header("Called on Trigger")]
    [SerializeField] private GameObject callbackObject = null;

    [SerializeField] private string method;

    private BoxCollider boxCollider;

    private MeshRenderer meshRenderer;

    [SerializeField] private GameObject _inventoryItem;

    [SerializeField] private Sprite itemSprite;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();

        boxCollider.enabled = false;
        meshRenderer.enabled = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.instance.SetQuestHint();

            if(callbackObject != null && method != "")
            {
                callbackObject.SendMessage(method);
            }

            InventoryItem inventoryItem = _inventoryItem.GetComponent<InventoryItem>();
            GameObject instance = inventoryItem.InstantiateItemOnInventory();

            Image itemImage = instance.gameObject.transform.GetChild(0).GetComponent<Image>();

            itemImage.sprite = itemSprite;

            Destroy(gameObject);
        }
    }

    public void TurnOnObjective()
    {
        boxCollider.enabled = true;
        meshRenderer.enabled = true;
    }
}
