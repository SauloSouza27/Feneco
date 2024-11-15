using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{

    public HealthBarScript healthBar;
    public static GameController instance;

    private GameObject player;

    public int maxHealthPoints { get; set; }

    public int armor { get; set; }
    public int vitality { get; set; }

    [SerializeField] private int healthPoints;

    private GameObject HUD;

    private TextMeshProUGUI healthPointsText;

    private bool isQuestActive = false;

    [SerializeField] private TextMeshProUGUI questHintText;

    [SerializeField] private string[] questHints;

    private int questHintIndex = 0;

    [SerializeField] private GameObject[] quests;
    
    [SerializeField] private TextMeshProUGUI damageStatus;
    [SerializeField] private TextMeshProUGUI armorStatus;
    [SerializeField] private TextMeshProUGUI vitalityStatus;

    private int questIndex = 0;

    public Renderer helmetRenderer;
    public Renderer armorRenderer;
    public Renderer swordHandRenderer;
    public Renderer swordBackRenderer;

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
        maxHealthPoints = healthPoints;
        HUD = GameObject.FindWithTag("HUD");
        damageStatus.text = $"{Slash.instance.damage}";

//        helmetRenderer = GameObject.FindWithTag("Helmet").GetComponent<Renderer>();
//        armorRenderer = GameObject.FindWithTag("Armor").GetComponent<Renderer>();
//        swordHandRenderer = GameObject.FindWithTag("Sword").GetComponent<Renderer>();
//        swordBackRenderer = GameObject.FindWithTag("SwordBack").GetComponent<Renderer>();
    }

    private void Start()
    {
        if (quests.Length > 0)
        {
            foreach (GameObject g in quests)
            {
                g.SetActive(false);
            }

            UpdateQuest();
        }

        UpdateHUD();
    }

    public void UpdateHUD()
    {
        //Update Quest Hint

        string printQuestText = "Quest Hint: ";

        if (healthPoints > maxHealthPoints)
        {
            healthPoints = maxHealthPoints;
        }

        if (!isQuestActive)
        {
            printQuestText += "No quest available";
        }
        else
        {
            printQuestText += questHints[questHintIndex];
        }

        questHintText.text = printQuestText;

        //Update HP Bar
        healthBar.SetMaxHealth(maxHealthPoints);
        healthBar.SetHealth(healthPoints);
    }

    public void UpdateHudStatus(int statusType)
    {
        if (statusType == 0)
        {
            damageStatus.text = $"{Slash.instance.damage}";
        }
        else if (statusType == 1)
        {
            vitalityStatus.text = $"{vitality}";
        }
        else 
        {
            armorStatus.text = $"{armor}";
            armorStatus.text = $"{armor}";
        }
        
    }
    
    public void TakeDamage(int damage)
    {
        int damageTaken = Mathf.Max(damage - armor, 0);

        healthPoints -= damageTaken;

        if (healthPoints <= 0)
        {
            GameOver();
        }

        UpdateHUD();
    }

    public void Heal(int lifeGain)
    {
        if (healthPoints < maxHealthPoints)
        {
            healthPoints += lifeGain;
            if (healthPoints >= maxHealthPoints)
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

        if (questIndex < quests.Length - 1)
        {
            questIndex++;
        }
    }
    public void UpdateEquipment(bool equipped, int equipmentType)
    {
        if (equipmentType == 0)
        {
            helmetRenderer.enabled = equipped;
        } 
        else if (equipmentType == 1)
        {
            armorRenderer.enabled = equipped;
        }
        else
        {
            swordHandRenderer.enabled = equipped;
            swordBackRenderer.enabled = !equipped;
        }
    }
}
