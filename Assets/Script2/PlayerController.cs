using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

namespace CyberMovementSystem
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        public float playerSpeed = 8.0f;
        [SerializeField]
        public float jumpHeight = 1.0f;
        [SerializeField]
        public float gravityValue = -9.81f;
        [SerializeField]
        private InputActionReference movementControl;
        [SerializeField]
        private InputActionReference jumpControl;
        private CharacterController charController; // Renamed to charController
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        [SerializeField]
        public float rotationSpeed = 4.0f;
        [SerializeField]
        public float sprintSpeed = 12.0f;
        float sprintTimer;
        [SerializeField]
        public float maxSprint = 15.0f;

        public Animator Animate; 
        private Transform cameraMain;



        public static bool Dialogue { get; set; } = false;

        private void OnEnable()
        {
            movementControl.action.Enable();
            jumpControl.action.Enable(); 
        }

        private void OnDisable()
        {
            movementControl.action.Disable();
            jumpControl.action.Disable();
        }

        private void Start()
        {
            charController = GetComponent<CharacterController>(); 
            cameraMain = Camera.main.transform;
           // Animate = GetComponent<Animator>();
        }

        void Update()
        {
            groundedPlayer = charController.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector2 movement = movementControl.action.ReadValue<Vector2>();
            Vector3 move = new Vector3(movement.x, 0, movement.y);
            move = cameraMain.forward * move.z + cameraMain.right * move.x;
            move.y = 0;
            charController.Move(move * playerSpeed * Time.deltaTime);
            Cursor.lockState = CursorLockMode.Locked;

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
                Animate.SetBool("IsMoving", true); 
            }
            else
            {
                Animate.SetBool("IsMoving", false); 
            }

            if (Input.GetKey(KeyCode.LeftShift) && sprintTimer > 0)
            {
                playerSpeed = sprintSpeed;
                sprintTimer -= Time.deltaTime;
            }
            else
            {
                playerSpeed = Mathf.Lerp(playerSpeed, maxSprint, Time.deltaTime);
                if (sprintTimer < maxSprint)
                {
                    sprintTimer += Time.deltaTime;
                }
            }

            if (jumpControl.action.triggered && groundedPlayer)
            {
                Animate.SetTrigger("Jump");
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            if (groundedPlayer)
            {
                Animate.ResetTrigger("Jump"); // Reset the "Jump" trigger when grounded
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            charController.Move(playerVelocity * Time.deltaTime);

            if (movement != Vector2.zero)
            {
                float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMain.eulerAngles.y;
                Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            }
        }

    }
}

