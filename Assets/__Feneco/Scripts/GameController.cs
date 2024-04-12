using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private GameObject player;

    private bool isQuestActive = false;

    private GameObject HUD;

    private TextMeshProUGUI questHintText;

    [SerializeField] private string[] questHints;

    private int questHintIndex = 0;

    private void Awake()
    {
        if (instance != null)
        {
            GameObject.Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        player = GameObject.FindWithTag("Player");
        HUD = GameObject.FindWithTag("HUD");
        questHintText = HUD.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        string printText = "Quest Hint: ";

        if (!isQuestActive)
        {
            printText += "No quest available";
        }
        else
        {
            printText += questHints[questHintIndex];
        }
        
        questHintText.text = printText;
    }

    public void SetQuestHint()
    {
        isQuestActive = true;
        UpdateHUD();
        questHintIndex++;
    }
    public void SetNoActiveQuest()
    {
        isQuestActive = false;
        UpdateHUD();
    }

    public void ObjectiveCompleted()
    {

    }
}
