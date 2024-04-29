using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum InventoryItemArchetype
{
    objective, healthPotion, granade
}
public class InventoryItem : MonoBehaviour
{
    [SerializeField] private string _name;

    [SerializeField] private InventoryItemArchetype inventoryItemArchetype;

    private Image _backGroundSprite, _itemSprite;

    private GameObject _inventoryContent;

    public Image BackGroundSprite
    {
        get { return _backGroundSprite; }
        set { _backGroundSprite = value; }
    }
    public Image ItemSprite
    {
        get { return _itemSprite; }
        set { _itemSprite = value; }
    }
    public virtual void ItemFunction()
    {

    }
    public GameObject InstantiateItemOnInventory()
    {
        if (_inventoryContent == null)
        {
            _inventoryContent = Inventory.instance.Content;
        }
        GameObject instance = Instantiate(gameObject);
        instance.transform.SetParent(_inventoryContent.transform, false);
        return instance;
    }
}
