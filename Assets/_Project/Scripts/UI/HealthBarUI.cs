using UnityEngine;
using PixDash.Player;

namespace PixDash.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        private Health playerHealth;

        private void Start()
        {
            // Busca al jugador en la escena de forma segura
            Player.Player player = Object.FindFirstObjectByType<Player.Player>();
            if (player != null)
            {
                playerHealth = player.GetComponent<Health>();
                if (playerHealth != null)
                {
                    // Nos conectamos al nuevo evento de vidas
                    playerHealth.onLivesChanged.AddListener(UpdateLivesDisplay);
                }
            }
        }

        private void UpdateLivesDisplay(int currentLives)
        {
            // Por ahora solo muestra en la consola cuántas vidas quedan
            Debug.Log($"[UI] El corazón de la interfaz sabe que te quedan: {currentLives} vidas.");
        }
    }
}