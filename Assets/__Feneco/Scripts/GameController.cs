using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private GameObject player;

    [SerializeField] public int maxHealthPoints { get; set; } = 1000;

    public int armor { get; set; } = 0;

    [SerializeField] private int healthPoints;

    private GameObject HUD;

    private TextMeshProUGUI healthPointsText;

    private bool isQuestActive = false;

    private TextMeshProUGUI questHintText;

    [SerializeField] private string[] questHints;

    private int questHintIndex = 0;

    [SerializeField] private GameObject[] quests;

    private int questIndex = 0;

    [SerializeField] private GameObject configMenu;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        player = GameObject.FindWithTag("Player");
        healthPoints = maxHealthPoints;
        HUD = GameObject.FindWithTag("HUD");
        questHintText = HUD.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        healthPointsText = HUD.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if(quests.Length > 0)
        {
            foreach (GameObject g in quests)
            {
                g.SetActive(false);
            }

            UpdateQuest();
        }
        
        UpdateHUD();

        configMenu.SetActive(true);
        configMenu.SetActive(false);
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
        damage -= armor;
        if(damage < 0)
        {
            damage = 0;
        }
        healthPoints -= damage;
        if(healthPoints == 0)
        {
            GameOver();
        }
        UpdateHUD();
    }
    public void Heal(int lifeGain)
    {
        if(healthPoints < maxHealthPoints)
        {
            healthPoints += lifeGain;
            if(healthPoints >= maxHealthPoints)
            {
                healthPoints = maxHealthPoints;
            }
            UpdateHUD();
        }
    }
    public void GameOver()
    {
        Time.timeScale = 0;
    }
    public void SetQuestHint()
    {
        isQuestActive = true;
        UpdateHUD();
        if (questHintIndex < questHints.Length - 1)
        {
            questHintIndex++;
        }
    }
    public void SetNoActiveQuest()
    {
        isQuestActive = false;
        UpdateHUD();
    }

    public void SpawnQuest(GameObject spawnQuest)
    {
        spawnQuest.SetActive(true);
    }

    public void UpdateQuest()
    {
        quests[questIndex].SetActive(true);

        if(questIndex < quests.Length - 1)
        {
            questIndex++;
        }
    }


    //atualizar armadura quando um item for equipado?
    //public void UpdateArmor()
    //{
        
    //}
}
