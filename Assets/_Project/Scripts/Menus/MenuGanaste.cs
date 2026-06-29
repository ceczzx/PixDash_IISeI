using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGanaste : MonoBehaviour
{
    public void JuegoNuevo()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void Volver()
    {
        SceneManager.LoadScene("MenuInicio");
    }
}
