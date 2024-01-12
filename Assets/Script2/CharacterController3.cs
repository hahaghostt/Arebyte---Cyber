
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController3 : MonoBehaviour
{
    float maxSpeed;
    public float normalSpeed = 10.0f;
    public float sprintSpeed = 20.0f;
    float rotation = 0.0f;
    float camRotation = 0.0f;
    GameObject cam;
    Rigidbody myRigidbody;
    public float jumpForce = 300.0f;

    public float rotationSpeed = 2.0f;
    public float camRotationSpeed = 1.5f;
    public float maxSprint = 5.0f;
    float sprintTimer;
    private float newVelocity = 1.0f;

    private Animator mAnimator;

    bool isOnGround;
    public GameObject groundChecker;
    public LayerMask groundLayer;


    public AudioClip backgroundMusic;
    public AudioSource musicPlayer;

    public static bool Dialogue { get; set; } = false;

    void Start()
    {
        sprintTimer = maxSprint;
        cam = GameObject.Find("Main Camera");
        myRigidbody = GetComponent<Rigidbody>();
        mAnimator = GetComponent<Animator>();
        musicPlayer.clip = backgroundMusic;
        musicPlayer.loop = true;
        musicPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {

        isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.5f, groundLayer);
        mAnimator.SetBool("isOnGround", isOnGround);

        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            mAnimator.SetTrigger("Jumped");
            myRigidbody.AddForce(transform.up * jumpForce);
        }
        Vector3 newVelocity = transform.forward * Input.GetAxis("Vertical") * maxSpeed;

        mAnimator.SetFloat("Speed", newVelocity.magnitude);

        myRigidbody.velocity = new Vector3(newVelocity.x, myRigidbody.velocity.y, newVelocity.z);

        if (Input.GetKey(KeyCode.LeftShift) && sprintTimer > 0.0f)
        {
            maxSpeed = sprintSpeed;
            sprintTimer = sprintTimer - Time.deltaTime;
        }
        else
        {
            maxSpeed = normalSpeed;
            if (Input.GetKey(KeyCode.LeftShift) == false)
            {
                sprintTimer = sprintTimer + Time.deltaTime;
            }
        }

        if (!Dialogue && Input.GetMouseButton(1))
        {
            rotation = rotation + Input.GetAxis("Mouse X") * rotationSpeed;
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));
            camRotation = Mathf.Clamp(camRotation, -40.0f, 40.0f);
            camRotation = camRotation + Input.GetAxis("Mouse Y") * camRotationSpeed;
            cam.transform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0.0f, 0.0f));
        }

        sprintTimer = Mathf.Clamp(sprintTimer, 0.0f, maxSprint);

        Vector3 sprintVelocity = (transform.forward * Input.GetAxis("Vertical") * maxSpeed) + (transform.right * Input.GetAxis("Horizontal") * maxSpeed);
        myRigidbody.velocity = new Vector3(sprintVelocity.x, myRigidbody.velocity.y, sprintVelocity.z);

       /* rotation = rotation + Input.GetAxis("Mouse X") * rotationSpeed;
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));
        camRotation = Mathf.Clamp(camRotation, -40.0f, 40.0f);
        camRotation = camRotation + Input.GetAxis("Mouse Y") * camRotationSpeed;
        cam.transform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0.0f, 0.0f)); */ 

    }
}
