using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum ItemArchetype
{
    objective, potionHealth
}
public class InventoryItem : MonoBehaviour
{
    [SerializeField] private string _name;

    [SerializeField] private ItemArchetype itemArchetype;

    private Image _backGroundSprite, _itemSprite;

    private GameObject inventoryContent;

    private GameObject sceneItem;

    public Image BackGroundSprite
    {
        get { return _backGroundSprite; }
        set { _backGroundSprite = value; }
    }

    private void Start()
    {
        inventoryContent = Inventory.instance.Content;
    }

    public void InstantiateItemOnInventory()
    {
        GameObject.Instantiate(gameObject, inventoryContent.transform);
    }
}
