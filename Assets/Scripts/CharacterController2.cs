using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CyberMovementSystem;

public class CharacterController2 : MonoBehaviour
{

    public float movementSpeed = 5f;
    public float rotationSpeed = 2f;

    private Rigidbody rb;
    private Camera playerCamera;

    private float cameraRotationX = 0f;
    private float cameraRotationY = 0f;

    public float jumpForce = 10f;
    public float gravityModifier;

    public bool isOnGround = true;

    private float maxCastDistance = 5f;
    private LayerMask obstacleLayer;

    static public bool dialogue = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        playerCamera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Physics.gravity *= gravityModifier;

        obstacleLayer = LayerMask.GetMask("obstacles");
    }

    private void Update()
    {
        HandleInput();
        RotateCamera();
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

        // Get the forward vector of the player's transform
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;

        // Normalize the direction to ensure consistent movement speed in all directions
        moveDirection.Normalize();

        Vector3 moveVelocity = moveDirection * movementSpeed;

        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        if (!CharacterController2.dialogue)
        {
            if (Input.GetMouseButton(1))
            {
                // Rotate the player around the Y-axis
                cameraRotationY += mouseX;
                transform.rotation = Quaternion.Euler(0f, cameraRotationY, 0f);

                // Rotate the camera around the X-axis
                cameraRotationX -= mouseY;
                cameraRotationX = Mathf.Clamp(cameraRotationX, -90f, 90f);
                playerCamera.transform.localRotation = Quaternion.Euler(cameraRotationX, 0f, 0f);

                if (Physics.SphereCast(playerCamera.transform.position, 10f, playerCamera.transform.forward, out RaycastHit hit, maxCastDistance, obstacleLayer))
                {
                    LimitCameraRotation(hit.distance);
                }
            }
        }
    }

    private void LimitCameraRotation(float distanceToObstacle)
    {
        float maxRotationX = Mathf.Atan(distanceToObstacle / 10f) * Mathf.Rad2Deg;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -maxRotationX, maxRotationX);

        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotationX, 0f, 0f);
    }

    private void FixedUpdate()
    {
        if (!dialogue)
        {
            MoveCharacter();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
}