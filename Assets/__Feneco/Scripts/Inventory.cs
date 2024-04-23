using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private GameObject inventoryScoll;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        inventoryScoll = transform.GetChild(1).gameObject;
    }
    public void UpdateItems()
    {
        
    }
    public void TurnOnOffInventory()
    {
        UpdateItems();
        inventoryScoll.SetActive(!inventoryScoll.activeSelf);
    }
}
