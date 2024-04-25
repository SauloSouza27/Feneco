using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum SceneItemArchetype
{
    objective, healthPotion
}
public class SceneItem : MonoBehaviour
{
    [SerializeField] private SceneItemArchetype sceneItemArchetype;

    [Header("Called on Trigger")]
    [SerializeField] private GameObject callbackObject = null;

    [SerializeField] private string method;

    private BoxCollider boxCollider;

    private MeshRenderer meshRenderer;

    [SerializeField] private GameObject inventoryItem;

    [SerializeField] private Sprite itemSprite;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();

        switch (sceneItemArchetype)
        {
            case SceneItemArchetype.objective:
                boxCollider.enabled = false;
                meshRenderer.enabled = false;
                break;
        }
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

            InventoryItem instanceInventoryItem = inventoryItem.GetComponent<InventoryItem>();
            GameObject instance = instanceInventoryItem.InstantiateItemOnInventory();

            Image itemImage = instance.transform.GetChild(0).GetComponent<Image>();

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
