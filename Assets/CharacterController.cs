using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CharacterController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 2f;

    private Rigidbody rb;
    private Camera mainCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

   
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {

        MoveCharacter();


        RotateCamera();
    }

    private void MoveCharacter()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 moveVelocity = moveDirection * movementSpeed;

        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

  
        transform.Rotate(Vector3.up * mouseX);

        float currentRotationX = mainCamera.transform.rotation.eulerAngles.x;
        float newRotationX = currentRotationX - mouseY;
        newRotationX = Mathf.Clamp(newRotationX, -90f, 90f);

        mainCamera.transform.rotation = Quaternion.Euler(newRotationX, 0f, 0f);
    }
}
