using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberMovementSystem
{
    public class CharacterController4: MonoBehaviour
    {
        private Animator animator;

        public static bool Dialogue = false; // Static variable to track if the player is in dialogue

        public float movementSpeed = 5f;
        public float rotationSpeed = 10f;
        public float jumpForce = 8f;
        public LayerMask groundMask;

        private bool isGrounded;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (!Dialogue)
            {
                HandleMovement();
                HandleJump();
            }
        }

        void HandleMovement()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                // Corrected line
                transform.position += moveDirection * movementSpeed * Time.deltaTime;

                // Set animation parameters
                animator.SetFloat("Speed", movementSpeed);
            }
            else
            {
                // Set animation parameters for idle
                animator.SetFloat("Speed", 0f);
            }
        }

        void HandleJump()
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f, groundMask);

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("Jump");
                transform.position += Vector3.up * jumpForce * Time.deltaTime;
            }
        }
    }
}
