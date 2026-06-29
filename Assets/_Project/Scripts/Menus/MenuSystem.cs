using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void Salir()
    {
        Application.Quit();
    }
}
