using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private GameObject inventoryConsumables, inventoryArmor;

    [SerializeField] private GameObject _content;

    public GameObject Content
    {
        get { return _content; }
    }

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

        inventoryConsumables = transform.GetChild(1).gameObject;
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(TurnOnOffInventory);
    }
    public void TurnOnOffInventory()
    {
        inventoryConsumables.SetActive(!inventoryConsumables.activeSelf);

        inventoryArmor.SetActive(!inventoryArmor.activeSelf);
    }
}
