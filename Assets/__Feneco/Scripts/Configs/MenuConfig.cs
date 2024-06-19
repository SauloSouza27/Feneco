using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuConfig : MonoBehaviour
{
    public static MenuConfig instance;

    private GameObject btMainMenu, btBack, btQuit;

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

        btBack = transform.GetChild(0).GetChild(transform.GetChild(0).childCount - 3).gameObject;
        btQuit = transform.GetChild(0).GetChild(transform.GetChild(0).childCount - 2).gameObject;
        btMainMenu = transform.GetChild(0).GetChild(transform.GetChild(0).childCount - 1).gameObject;

        btMainMenu.GetComponent<Button>().onClick.AddListener(RetrunToMenu);
    }

    public void StartMenuConfig(bool turnOn)
    {
        gameObject.GetComponent<Canvas>().enabled = turnOn;
        if(turnOn)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (SceneManager.GetActiveScene().buildIndex != 0 || SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1)
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
            btQuit.SetActive(false);
        }
        else
        {
            btMainMenu.SetActive(true);
            btQuit.SetActive(true);
        }
    }

    public void RetrunToMenu()
    {
        SceneManager.LoadScene(0);
        gameObject.GetComponent<Canvas>().enabled = false;

    }

    public void SairJogo(){
        Debug.Log ("Sair do Jogo");
        Application.Quit();
    }
}
