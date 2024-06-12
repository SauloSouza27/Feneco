using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeLevel;
    [SerializeField]private GameObject painelMenuInicial;
    [SerializeField]private GameObject painelOpcoes;
    // Start is called before the first frame update
    public void Jogar(){
        SceneManager.LoadScene("Fase1");

    }

    public void AbrirOpcoes(){
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);

    }

    public void FecharOpcoes(){
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);

    }

    public void SairJogo(){
        Debug.Log ("Sair do Jogo");
        Application.Quit();
    }

    public void AbrirCreditos(){

    }
}
