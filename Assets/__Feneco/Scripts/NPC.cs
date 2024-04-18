using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject dialogueScreen;

    [SerializeField] private GameObject questScreen;

    [SerializeField] private Sprite avatar;

    [SerializeField] private string[] dialogue;

    private int dialogueIndex = 0;

    [SerializeField] private bool isQuest = false;

    [SerializeField] private string[] questText;

    private bool isTalkEnd = false;

    private bool acceptedMission = false;

    private bool isQuestCompleted = false;

    [SerializeField] private string[] questCompletedDialogue;

    [SerializeField] private GameObject callbackObject = null;

    [SerializeField] private string method;

    [SerializeField] private GameObject spawnQuest = null;

    private Animator npc_fsm;

    private PlayerController playerController = null;

    
    private void Awake()
    {
        npc_fsm = transform.GetComponent<Animator>();
    }

    private void Start()
    {
        dialogueScreen.transform.GetChild(2).GetComponent<Image>().sprite = avatar;
        dialogueScreen.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(BtBack);
        dialogueScreen.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(BtNext);
        questScreen.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(BtYes);
        questScreen.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(BtNo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.SetNPCNearON(true, gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (playerController != null)
            {
                playerController.SetNPCNearON(false, null);  
            }

            if (!acceptedMission)
            {
                isTalkEnd = false;
                dialogueIndex = 0;
            }

            npc_fsm.SetBool("IsTalking", false);
            dialogueScreen.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void TalkNPC()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        npc_fsm.SetBool("IsTalking", true);
        dialogueIndex = 0;
        dialogueScreen.SetActive(true);
        dialogueScreen.transform.GetChild(3).GetComponent<Button>().interactable = false;
        BuildDialogue();
    }

    private void BuildDialogue()
    {
        if (!isQuestCompleted)
        {
            if (!isTalkEnd)
            {
                dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = dialogue[dialogueIndex];
            }
            else
            {
                dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = questText[3];
            }

            if (isQuest)
            {
                questScreen = dialogueScreen.transform.GetChild(5).gameObject;
            }
        }
        else
        {
            dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = questCompletedDialogue[0];
        }
        
    }

    private void BtBack()
    {
        if (!isTalkEnd)
        {
            if (dialogueIndex == 1)
            {
                dialogueScreen.transform.GetChild(3).GetComponent<Button>().interactable = false;
            }

            if (dialogueIndex == dialogue.Length - 1)
            {
                dialogueScreen.transform.GetChild(4).GetComponent<Button>().interactable = true;
            }

            if (dialogueIndex > 0)
            {
                dialogueIndex--;
                dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = dialogue[dialogueIndex];

                if (questScreen.activeSelf)
                {
                    questScreen.SetActive(false);
                }
            }
        }
        else
        {
            if (playerController != null)
            {
                playerController.SetNPCNearON(false, null);
            }

            npc_fsm.SetBool("IsTalking", false);
            dialogueScreen.SetActive(false);
        }
    }

    private void BtNext()
    {
        if (!isTalkEnd)
        {
            if (dialogueIndex == 0)
            {
                dialogueScreen.transform.GetChild(3).GetComponent<Button>().interactable = true;
            }

            if (dialogueIndex < dialogue.Length - 1)
            {
                dialogueIndex++;
                dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = dialogue[dialogueIndex];

                if (dialogueIndex == dialogue.Length - 1)
                {
                    dialogueScreen.transform.GetChild(4).GetComponent<Button>().interactable = false;
                    questScreen.SetActive(true);
                    questScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questText[0];
                }
            }
        }
        else
        {
            if (playerController != null)
            {
                playerController.SetNPCNearON(false, null);
            }

            npc_fsm.SetBool("IsTalking", false);
            dialogueScreen.SetActive(false);

            if (isQuestCompleted)
            {
                GameController.instance.SetNoActiveQuest();
                GameController.instance.ObjectiveCompleted();
            }
        }
        
    }

    private void BtYes()
    {
        questScreen.SetActive(false);
        
        dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = questText[1];
        dialogueScreen.transform.GetChild(3).GetComponent<Button>().interactable = false;
        dialogueScreen.transform.GetChild(4).GetComponent<Button>().interactable = true;
       
        isTalkEnd = true;
        acceptedMission = true;
       
        GameController.instance.SetQuestHint();

        if (callbackObject != null && method != "")
        {
            if (!spawnQuest)
            {
                callbackObject.SendMessage(method);
            }
            else
            {
                callbackObject.SendMessage(method, spawnQuest);
            }
        }
    }

    private void BtNo()
    {
        questScreen.SetActive(false);
        
        dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = questText[2];
        dialogueScreen.transform.GetChild(3).GetComponent<Button>().interactable = false;
        dialogueScreen.transform.GetChild(4).GetComponent<Button>().interactable = true;
       
        isTalkEnd = true;
    }

    public void CompleteQuest()
    {
        isQuestCompleted = true;
    }
}
