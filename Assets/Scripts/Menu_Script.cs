using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{
    public void Jogar()
    {
        SceneManager.LoadScene("Game");
    }
    public void Sair()
    {
        Application.Quit();
    }
    public void Instrucoes()
    {
        SceneManager.LoadScene("Instruções");
    }
    public void Voltar()
    {
        SceneManager.LoadScene("Menu");
    }
}