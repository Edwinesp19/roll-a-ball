using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("nivel-1");
    }
    public void Opciones()
    {
        SceneManager.LoadScene("MenuOpciones");
    }
    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
    public void Atras()
    {
        SceneManager.LoadScene("MenuInicio"); 
    }
}
