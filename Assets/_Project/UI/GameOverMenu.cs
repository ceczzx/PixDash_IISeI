using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameOverMenu : MonoBehaviour
{
    // Ahora arrastraremos el Panel aquí para ocultarlo o mostrarlo
    public GameObject panelGameOver;

    private void Start()
    {
        // Al empezar, solo ocultamos el panel visual
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(false);
        }
    }

    public void ActivarMenuGameOver()
    {
        // Al morir, encendemos el panel visual
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true); 
        }
    }

    public void ReintentarNivel()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}