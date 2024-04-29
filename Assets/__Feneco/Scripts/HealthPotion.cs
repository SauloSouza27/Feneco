using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPotion : InventoryItem
{
    [SerializeField] private int lifeGain;

    private Button button;

    private void Awake()
    {
        button = transform.GetChild(0).transform.GetComponent<Button>();

        button.onClick.AddListener(ItemFunction);
    }
    public override void ItemFunction()
    {
        GameController.instance.Heal(lifeGain);

        Destroy(gameObject);
    }
}
