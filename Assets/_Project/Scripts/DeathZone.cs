using UnityEngine;
using PixDash.Player;
using UnityEngine.SceneManagement;
public class DeathZone : MonoBehaviour
{
    // Unity ejecuta esto automáticamente cuando algo cruza el Box Collider 2D (Is Trigger)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Revisa si lo que cayó tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            // Busca el componente de salud en el jugador
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                // Le resta una vida y lo manda al inicio
                playerHealth.KillByPrecipice();
            }
        }
    }
}