using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    public void ShowGameOverScreen() {
        gameObject.SetActive(true);
    }
    
    public void Retry() {
        SceneManager.LoadScene(1);
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }
}