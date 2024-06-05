using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [Header("UI")]
    private Image image;
    private TextMeshProUGUI countText;

    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    private void Awake()
    {
        image = transform.GetComponent<Image>();
        if (image == null)
        {
            Debug.LogError("Image component not found on InventoryItem GameObject.");
        }

        countText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (countText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on InventoryItem's child GameObject.");
        }
    }
    public void InitialiseItem(Item newItem)
    {
         if (newItem == null)
        {
            Debug.LogError("newItem is null in InitialiseItem.");
            return;
        }

        if (newItem.image == null)
        {
            Debug.LogError("newItem.image is null in InitialiseItem.");
            return;
        }

        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }

    

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}
