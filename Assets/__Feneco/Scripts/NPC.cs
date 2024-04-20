using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class NPC : MonoBehaviour
{
    private NPC instanceNPC;

    [SerializeField] private new string name;

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

    private bool isQuestUpdated = false;

    private bool isDelivered = false;

    [SerializeField] private string[] questCompletedDialogue;

    [SerializeField] private GameObject callbackObject = null;

    [SerializeField] private string method;

    private Animator npc_fsm;

    private PlayerController playerController = null;


    public void Awake()
    {
        instanceNPC = gameObject.GetComponent<NPC>();
        npc_fsm = transform.GetComponent<Animator>();
    }

    public void OnEnable()
    {
        isTalkEnd = false;
        acceptedMission = false;
        isQuestCompleted = false;
        isQuestUpdated = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.SetNPCNearON(true, gameObject);
            }
        }

        if(other.name == "Area Deliver Escort")
        {
            isDelivered = true;
            CompleteQuest();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (playerController != null)
            {
                playerController.SetNPCNearON(false, null);  
            }

            if (isQuestCompleted && !isQuestUpdated)
            {
                GameController.instance.SetNoActiveQuest();
                GameController.instance.UpdateQuest();

                isQuestUpdated = true;
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

        BuildButtons();
        BuildDialogue();
    }

    public void BuildDialogue()
    {
        if (!isQuestCompleted && instanceNPC.dialogue.Length != 0)
        {
            if (!isTalkEnd)
            {
                dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name + ": " + instanceNPC.dialogue[dialogueIndex];
            }
            else
            {
                dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name + ": " + instanceNPC.questText[3];
            }

            if (isQuest)
            {
                questScreen = dialogueScreen.transform.GetChild(5).gameObject;
            }
        }
        else
        {
            dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name + ": " + instanceNPC.questCompletedDialogue[0];
        }
        
    }

    public void BuildButtons()
    {
        dialogueScreen.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
        dialogueScreen.transform.GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
        questScreen.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        questScreen.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();

        dialogueScreen.transform.GetChild(2).GetComponent<Image>().sprite = instanceNPC.avatar;
        dialogueScreen.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(instanceNPC.BtBack);
        dialogueScreen.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(instanceNPC.BtNext);
        questScreen.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(instanceNPC.BtYes);
        questScreen.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(instanceNPC.BtNo);
    }

    public void BtBack()
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
                dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name + ": " + instanceNPC.dialogue[dialogueIndex];

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

    public void BtNext()
    {
        if (!isTalkEnd)
        {
            if (dialogueIndex == 0)
            {
                dialogueScreen.transform.GetChild(3).GetComponent<Button>().interactable = true;
            }

            if (dialogueIndex < instanceNPC.dialogue.Length - 1)
            {
                dialogueIndex++;
                dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name + ": " + instanceNPC.dialogue[dialogueIndex];

                if (dialogueIndex == instanceNPC.dialogue.Length - 1)
                {
                    dialogueScreen.transform.GetChild(4).GetComponent<Button>().interactable = false;
                    questScreen.SetActive(true);
                    questScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = instanceNPC.questText[0];
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

            if (isQuestCompleted && !isQuestUpdated)
            {
                GameController.instance.SetNoActiveQuest();
                GameController.instance.UpdateQuest();

                isQuestUpdated = true;
            }
        }
        
    }

    public void BtYes()
    {
        questScreen.SetActive(false);
        
        dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name + ": " + instanceNPC.questText[1];
        dialogueScreen.transform.GetChild(3).GetComponent<Button>().interactable = false;
        dialogueScreen.transform.GetChild(4).GetComponent<Button>().interactable = true;
       
        isTalkEnd = true;
        acceptedMission = true;
       
        GameController.instance.SetQuestHint();

        if (callbackObject != null && method != "")
        {
            callbackObject.SendMessage(method);
        }
    }

    public void BtNo()
    {
        questScreen.SetActive(false);
        
        dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name + ": " + instanceNPC.questText[2];
        dialogueScreen.transform.GetChild(3).GetComponent<Button>().interactable = false;
        dialogueScreen.transform.GetChild(4).GetComponent<Button>().interactable = true;
       
        isTalkEnd = true;
    }

    public void CompleteQuest()
    {
        isQuestCompleted = true;
    }
    public bool GetAcceptedMission()
    {
        return acceptedMission;
    }
    public bool GetIsQuestUpdated()
    {
        return isQuestUpdated;
    }
    public bool GetIsDelivered()
    {
        return isDelivered;
    }
    public GameObject GetCallbackObject()
    {
        return callbackObject;
    }
}
