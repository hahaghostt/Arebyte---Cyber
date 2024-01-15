using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace CyberMovementSystem
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        public float playerSpeed = 2.0f;
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

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            if (!Dialogue)
            {
                movementControl.action.Enable();
                jumpControl.action.Enable(); 
            }

           
            if (jumpControl.action.triggered && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
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

