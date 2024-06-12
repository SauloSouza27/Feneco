using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuConfig : MonoBehaviour
{
    public static MenuConfig instance;

    private GameObject btMainMenu, btBack;

    private GameObject player;

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

        btBack = transform.GetChild(0).GetChild(transform.GetChild(0).childCount - 2).gameObject;
        btMainMenu = transform.GetChild(0).GetChild(transform.GetChild(0).childCount - 1).gameObject;

        btMainMenu.GetComponent<Button>().onClick.AddListener(RetrunToMenu);
    }

    public void StartMenuConfig(bool turnOn)
    {
        gameObject.GetComponent<Canvas>().enabled = turnOn;

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if(player!= null)
        {
            btBack.GetComponent<Button>().onClick.AddListener(player.GetComponent<PlayerController>().UnfreezeCamera);
            btBack.GetComponent<Button>().onClick.AddListener(player.GetComponent<PlayerController>().SetIsPausedOFF);
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {

            btMainMenu.SetActive(false);
        }
        else
        {
            btMainMenu.SetActive(true);
        }
    }

    public void RetrunToMenu()
    {
        SceneManager.LoadScene(0);
        gameObject.GetComponent<Canvas>().enabled = false;

    }
}
