using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private GameObject player;

    private int healthPoints = 10;

    private GameObject HUD;

    private TextMeshProUGUI healthPointsText;

    private bool isQuestActive = false;

    private TextMeshProUGUI questHintText;

    [SerializeField] private string[] questHints;

    private int questHintIndex = 0;

    [SerializeField] private GameObject[] quests;

    private int questIndex = 0;

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
        healthPointsText = HUD.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        foreach(GameObject g in quests)
        {
            g.SetActive(false);
        }
        UpdateQuest();
        UpdateHUD();
    }
    public void UpdateHUD()
    {
        string printQuestText = "Quest Hint: ";
        string printHealthPointsText = "HP: ";

        healthPointsText.text = printHealthPointsText + healthPoints;

        if (!isQuestActive)
        {
            printQuestText += "No quest available";
        }
        else
        {
            printQuestText += questHints[questHintIndex];
        }
        
        questHintText.text = printQuestText;
    }
    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if(healthPoints == 0)
        {
            GameOver();
        }
        UpdateHUD();
    }
    public void GameOver()
    {
        Time.timeScale = 0;
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

    public void SetActiveQuest(GameObject spawnQuest)
    {
        spawnQuest.SetActive(true);
    }

    public void UpdateQuest()
    {
        quests[questIndex].SetActive(true);
        questIndex++;
    }
}
