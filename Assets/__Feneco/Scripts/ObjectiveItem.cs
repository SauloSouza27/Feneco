using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveItem : MonoBehaviour
{
    [Header("Called on Trigger")]
    [SerializeField] private GameObject callbackObject = null;

    [SerializeField] private string method;

    private BoxCollider boxCollider;

    private MeshRenderer meshRenderer;

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
            Destroy(gameObject);

            GameController.instance.SetQuestHint();

            if(callbackObject != null && method != "")
            {
                callbackObject.SendMessage(method);
            }
        }
    }

    public void TurnOnObjective()
    {
        boxCollider.enabled = true;
        meshRenderer.enabled = true;
    }
}
