using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using Cinemachine;

public class NPC : MonoBehaviour
{
    private NPC instance;

    [SerializeField] private new string name;

    [SerializeField] private GameObject dialogueScreen;

    [SerializeField] private GameObject questScreen;

    [SerializeField] private Sprite avatar;

    [SerializeField] private string[] dialogue;

    private int dialogueIndex = 0;

    [SerializeField] private bool isQuest = false;

    [SerializeField] private string[] questText;

    private bool isTalkEnd = false;

    public bool acceptedMission { get; private set; } = false;

    public bool isQuestCompleted { get; private set; } = false;

    public bool isQuestUpdated { get; private set; } = false;

    public bool isDelivered { get; private set; } = false;

    [SerializeField] private string[] questCompletedDialogue;

    [Header("Called When Accept Mission")]
    [SerializeField] private GameObject callbackObject = null;
    [SerializeField] private string method;
    public ObjectiveItem receivedItem { get; set; } = null;

    private Animator npc_fsm;

    private PlayerController playerController = null;

    // Camera Control

    private CinemachineVirtualCamera virtualCamera;

    public GameObject CallbackObject
    {
        get { return instance.callbackObject; }
    }

    public void Awake()
    {
        instance = gameObject.GetComponent<NPC>();
        npc_fsm = transform.GetComponent<Animator>();
    }

    public void OnEnable()
    {
        instance.isTalkEnd = false;
        instance.acceptedMission = false;
        instance.isQuestCompleted = false;
        instance.isQuestUpdated = false;
        instance.isDelivered = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(playerController == null)
            {
                playerController = other.GetComponent<PlayerController>();
            }

            if (playerController != null)
            {
                playerController.SetNPCNearON(true, gameObject);

                virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();

                if (virtualCamera != null)
                {
                    //virtualCamera.
                }
                else
                {
                    Debug.Log("`Virtual Camera not found!");
                }
            }
        }

        if(other.name == "Area Deliver Escort")
        {
            instance.isDelivered = true;
            CompleteQuest();
        }
    }
    private void Update()
    {
        //Debug.Log(isDelivered);
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (playerController != null)
            {
                playerController.SetNPCNearON(false, null);
                playerController.UnfreezeCamera();
            }

            if (instance.isQuestCompleted && !isQuestUpdated)
            {
                GameController.instance.SetNoActiveQuest();
                GameController.instance.UpdateQuest();

                isQuestUpdated = true;
            }

            if (!instance.acceptedMission)
            {
                instance.isTalkEnd = false;
                instance.dialogueIndex = 0;
            }

            npc_fsm.SetBool("isTalking", false);
            dialogueScreen.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void TalkNPC()
    {
        transform.GetChild(0).gameObject.SetActive(false);

        npc_fsm.SetBool("isTalking", true);

        instance.dialogueIndex = 0;
        dialogueScreen.SetActive(true);
        dialogueScreen.transform.GetChild(3).GetComponent<Button>().interactable = false;

        BuildButtons();
        BuildDialogue();
    }

    public void BuildDialogue()
    {
        if (!instance.isQuestCompleted && instance.dialogue.Length != 0)
        {
            if (!instance.isTalkEnd)
            {
                dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = instance.name + ": " + instance.dialogue[dialogueIndex];
            }
            else
            {
                dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = instance.name + ": " + instance.questText[3];
            }

            if (instance.isQuest)
            {
                questScreen = dialogueScreen.transform.GetChild(5).gameObject;
            }
        }
        else
        {
            dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = instance.name + ": " + instance.questCompletedDialogue[0];
        }
        
    }

    public void BuildButtons()
    {
        dialogueScreen.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
        dialogueScreen.transform.GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
        questScreen.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        questScreen.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();

        dialogueScreen.transform.GetChild(2).GetComponent<Image>().sprite = instance.avatar;
        dialogueScreen.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(instance.BtBack);
        dialogueScreen.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(instance.BtNext);
        questScreen.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(instance.BtYes);
        questScreen.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(instance.BtNo);
    }

    public void BtBack()
    {
        if (!isTalkEnd)
        {
            if (instance.dialogueIndex == 1)
            {
                dialogueScreen.transform.GetChild(3).GetComponent<Button>().interactable = false;
            }

            if (instance.dialogueIndex == dialogue.Length - 1)
            {
                dialogueScreen.transform.GetChild(4).GetComponent<Button>().interactable = true;
            }

            if (instance.dialogueIndex > 0)
            {
                instance.dialogueIndex--;
                dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = instance.name + ": " + instance.dialogue[dialogueIndex];

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
                playerController.UnfreezeCamera();
            }

            npc_fsm.SetBool("isTalking", false);
            dialogueScreen.SetActive(false);
        }
    }

    public void BtNext()
    {
        if (!isTalkEnd)
        {
            if (instance.dialogueIndex == 0)
            {
                dialogueScreen.transform.GetChild(3).GetComponent<Button>().interactable = true;
            }

            if (instance.dialogueIndex < instance.dialogue.Length - 1)
            {
                instance.dialogueIndex++;
                dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = instance.name + ": " + instance.dialogue[dialogueIndex];

                if (instance.dialogueIndex == instance.dialogue.Length - 1)
                {
                    dialogueScreen.transform.GetChild(4).GetComponent<Button>().interactable = false;
                    questScreen.SetActive(true);
                    questScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = instance.questText[0];
                }
            }
        }
        else
        {
            if (playerController != null)
            {
                playerController.SetNPCNearON(false, null);
                playerController.UnfreezeCamera();
            }

            npc_fsm.SetBool("isTalking", false);
            dialogueScreen.SetActive(false);

            if (instance.isQuestCompleted && !instance.isQuestUpdated)
            {
                GameController.instance.SetNoActiveQuest();
                GameController.instance.UpdateQuest();

                if(receivedItem != null)
                {
                    receivedItem.DeliverItemNPC();
                }

                instance.isQuestUpdated = true;
            }
        }
        
    }

    public void BtYes()
    {
        questScreen.SetActive(false);
        
        dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name + ": " + instance.questText[1];
        dialogueScreen.transform.GetChild(3).GetComponent<Button>().interactable = false;
        dialogueScreen.transform.GetChild(4).GetComponent<Button>().interactable = true;

        instance.isTalkEnd = true;
        instance.acceptedMission = true;
        SFXManager.instance.PlaySFX(SFXManager.SFXType.AcceptQuest);
       
        GameController.instance.SetQuestHint();

        if (callbackObject != null && method != "")
        {
            instance.callbackObject.SendMessage(method);
        }
    }

    public void BtNo()
    {
        questScreen.SetActive(false);
        
        dialogueScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = instance.name + ": " + instance.questText[2];
        dialogueScreen.transform.GetChild(3).GetComponent<Button>().interactable = false;
        dialogueScreen.transform.GetChild(4).GetComponent<Button>().interactable = true;

        instance.isTalkEnd = true;
    }

    public void CompleteQuest()
    {
        instance.isQuestCompleted = true;
    }
}
