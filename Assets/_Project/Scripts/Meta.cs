using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Meta : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject, 0.5f);
            SceneManager.LoadScene("MenuGanaste");
        }
    }

}
