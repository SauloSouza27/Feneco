using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject dialogueScreen;

    [SerializeField] private GameObject questScreen;

    [SerializeField] private Sprite avatar;

    [SerializeField] private string[] dialogue;

    private int dialogueIndex = 0;

    [SerializeField] private GameObject callbackObject = null;

    [SerializeField] private string metodo;

    private Animator npc_fsm;

    private PlayerController playerController = null;

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        npc_fsm = transform.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (playerController != null)
            {
                playerController = other.GetComponent<PlayerController>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            npc_fsm.SetBool("IsTalking", false);
            dialogueScreen.SetActive(false);
        }
    }

    public void TalkNPC()
    {
        npc_fsm.SetBool("IsTalking", true);
        dialogueIndex = 0;
        dialogueScreen.SetActive(true);
        BuildDialogue();
    }

    private void BuildDialogue()
    {

    }


}
