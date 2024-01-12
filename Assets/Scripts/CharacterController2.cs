using UnityEngine;

namespace CyberMovementSystem
{
    public class CharacterController2 : MonoBehaviour
    {
        public float movementSpeed = 5f;
        public float rotationSpeed = 2f;

        private Rigidbody rb;
        private Camera playerCamera;

        private float cameraRotationX = 0f;

        public float jumpForce = 10f;
        public float gravityModifier;

        public bool isOnGround = true;

        private float maxCastDistance = 5f;
        private LayerMask obstacleLayer;

        private float turnSmoothVelocity;  // Variable for smooth rotation
        public float turnSmoothTime = 0.1f;  // Time taken to smooth the rotation

        private Animator animator;
        private float angle;

        public static bool Dialogue { get; set; } = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            playerCamera = Camera.main;

            // Create an empty GameObject to act as a parent for both player and camera
            GameObject playerContainer = new GameObject("PlayerContainer");

            transform.parent = playerContainer.transform;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Physics.gravity *= gravityModifier;

            obstacleLayer = LayerMask.GetMask("obstacles");

            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            HandleInput();
            RotateCamera();
            UpdateAnimations();
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (Input.GetMouseButtonDown(1))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }
        }

        private void MoveCharacter()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
            Vector3 moveVelocity = moveDirection * movementSpeed;



           
            transform.parent.rotation = Quaternion.Euler(0f, angle, 0f);

            // Apply the velocity
            rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
        }

        private void RotateCamera()
        {
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;

            if (!Dialogue && Input.GetMouseButton(1))
            {
                cameraRotationX -= mouseY;
                cameraRotationX = Mathf.Clamp(cameraRotationX, -90f, 90f);
                playerCamera.transform.localRotation = Quaternion.Euler(cameraRotationX, 0f, 0f);

                // Check for camera collisions
                if (Physics.SphereCast(playerCamera.transform.position, 10f, playerCamera.transform.forward, out RaycastHit hit, maxCastDistance, obstacleLayer))
                {
                    LimitCameraRotation(hit.distance);
                }
            }
        }


        private void LimitCameraRotation(float distanceToObstacle)
        {
            float maxRotationX = Mathf.Atan(distanceToObstacle / 100f) * Mathf.Rad2Deg;
            cameraRotationX = Mathf.Clamp(cameraRotationX, -maxRotationX, maxRotationX);

            playerCamera.transform.localRotation = Quaternion.Euler(cameraRotationX, 0f, 0f);
        }

        private void FixedUpdate()
        {
            if (!Dialogue)
            {
                MoveCharacter();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            isOnGround = true;
        }

        private void UpdateAnimations()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            bool isMoving = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;

            animator.SetBool("IsMoving", isMoving);

            if (isMoving)
            {
                Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
            }

            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                animator.SetTrigger("Jump");
            }
        }
    }
}
