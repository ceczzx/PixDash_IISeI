using UnityEngine;
using UnityEngine.Events;

namespace PixDash.Player
{
    public class Health : MonoBehaviour
    {
        public int maxLives = 3;
        public int currentLives;
        public bool isDead = false;

        public UnityEvent<int> onLivesChanged = new UnityEvent<int>();
        public UnityEvent onDeath = new UnityEvent();

        private Vector3 startPosition;
        private Rigidbody2D rb;

        private void Start()
        {
            currentLives = maxLives;
            onLivesChanged?.Invoke(currentLives); 

            startPosition = transform.position;
            rb = GetComponent<Rigidbody2D>();
        }

        // Nueva función para el botón 'K': Restablece todo y vuelve al inicio sin penalización
        public void ResetToStartFullHealth()
        {
            if (isDead) return;

            currentLives = maxLives; // Vuelve a tener 3 vidas completas
            onLivesChanged?.Invoke(currentLives);

            RespawnPlayer();
            Debug.Log($"[Prueba K] Jugador reiniciado al inicio con vida completa: {currentLives}/{maxLives}");
        }

        // Esta se mantiene para la caída real del precipicio
        public void KillByPrecipice()
        {
            if (isDead) return;

            currentLives -= 1;
            currentLives = Mathf.Max(currentLives, 0);

            Debug.Log($"¡El jugador cayó al vacío! Vidas restantes: {currentLives}");
            onLivesChanged?.Invoke(currentLives);

            if (currentLives <= 0)
            {
                DieCompletely();
            }
            else
            {
                RespawnPlayer();
            }
        }

        private void RespawnPlayer()
        {
            // Cambiado por 'velocity' clásico para evitar incompatibilidades en tu versión
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }

            transform.position = startPosition;
            Debug.Log("Jugador teletransportado a la posición inicial.");
        }

        private void DieCompletely()
        {
            if (isDead) return;

            isDead = true;
            Debug.Log("¡GAME OVER! Ya no quedan vidas.");
            onDeath?.Invoke();

            // Esto buscará el script nuevo que acabamos de crear y activará el cartel
            GameOverMenu menu = Object.FindFirstObjectByType<GameOverMenu>();
            if (menu != null)
            {
                menu.ActivarMenuGameOver();
            }

            Time.timeScale = 0f; // Congela el juego
        }
    }
}