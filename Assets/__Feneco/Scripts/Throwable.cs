using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Throwable : InventoryItem
{

    private Button button;

    private void Awake()
    {
        button = transform.GetChild(0).transform.GetComponent<Button>();

        button.onClick.AddListener(ItemFunction);
    }
    public override void ItemFunction()
    {

        Destroy(gameObject);
    }
}
