using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_1 : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Transform cam;
    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float test;

    private float lasttarget;
    private float startTargetRotation;
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if(direction.magnitude > 0f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float curretnTargetAngle = test = Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle);

            if (lasttarget != targetAngle)
            {
                lasttarget = targetAngle;
                startTargetRotation = curretnTargetAngle;
            }

            animator.SetFloat("Rotation", curretnTargetAngle,0.15f,Time.deltaTime);
            animator.SetFloat("Start Rotation", startTargetRotation,0.05f,Time.deltaTime);
        }

        animator.SetFloat("Speed", direction.magnitude,0.2f,Time.deltaTime);
        animator.stabilizeFeet = true;

    }

}
