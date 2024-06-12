using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeLevel;
    [SerializeField]private GameObject painelMenuInicial;
    
    public void Jogar(){
        SceneManager.LoadScene(1);
    }

    public void AbrirOpcoes(){
        //painelMenuInicial.SetActive(false);
        MenuConfig.instance.StartMenuConfig(true);
    }

    public void FecharOpcoes(){
        //painelOpcoes.SetActive(false);
        MenuConfig.instance.StartMenuConfig(false);
    }

    public void SairJogo(){
        Debug.Log ("Sair do Jogo");
        Application.Quit();
    }

    public void AbrirCreditos(){

    }
}
