using UnityEngine;
using UnityEngine.InputSystem;

namespace PixDash.Player
{
    public class Player : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float jumpForce = 10f;
        public Transform groundCheck;
        public LayerMask groundLayer;
        public float groundRadius = 0.2f;
        public float punchCooldown = 0.5f;

        private Animator animator;
        private Rigidbody2D rb;
        private Health playerHealth;
        private float horizontal;
        private bool isGrounded;
        private bool FacingRight = true;
        private float lastPunchTime = -999f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            playerHealth = GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.onDeath.AddListener(OnPlayerDeath);
            }
        }

        private void Update()
        {
            if (playerHealth != null && playerHealth.isDead) return;

            // Movimiento básico
            if (Keyboard.current != null)
            {
                horizontal = Keyboard.current.aKey.isPressed ? -1 :
                             Keyboard.current.dKey.isPressed ? 1 : 0;
            }
            else
            {
                horizontal = Input.GetAxisRaw("Horizontal");
            }

            if ((horizontal == -1 && FacingRight == true) || (horizontal == 1 && FacingRight == false))
            {
                Flip();
            }

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

            // Salto
            bool spacePressed = (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) || Input.GetKeyDown(KeyCode.Space);
            if (spacePressed && isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            // Ataque
            bool punchPressed = Input.GetMouseButtonDown(0) || (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame);
            if (punchPressed)
            {
                TryPunch();
            }

            // --- CORRECCIÓN K: Llama al nuevo reinicio completo ---
            bool kPressed = Input.GetKeyDown(KeyCode.K) || (Keyboard.current != null && Keyboard.current.kKey.wasPressedThisFrame);
            if (kPressed && playerHealth != null)
            {
                playerHealth.ResetToStartFullHealth();
            }

            // Animaciones (Corregido a rb.velocity)
            float speed = Mathf.Abs(horizontal * moveSpeed);
            animator.SetFloat("Horizontal Speed", speed);
            animator.SetBool("isGrounded", isGrounded);
            animator.SetFloat("Vertical Speed", rb.linearVelocity.y);
        }

        private void FixedUpdate()
        {
            if (playerHealth != null && playerHealth.isDead) return;
            // Corregido a rb.velocity
            rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
        }

        private void TryPunch()
        {
            if (Time.time < lastPunchTime + punchCooldown) return;
            lastPunchTime = Time.time;
            animator.SetTrigger("Punch");
        }

        private void OnPlayerDeath()
        {
            animator.SetTrigger("Death");
            rb.linearVelocity = Vector2.zero;
        }

        private void Flip()
        {
            FacingRight = !FacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}