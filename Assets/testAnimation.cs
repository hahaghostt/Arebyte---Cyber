using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class testAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] [Range(0,1)]private float speed;
    [SerializeField] bool jog;
    [SerializeField] bool stabilizefoot;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.stabilizeFeet = stabilizefoot;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", jog ? Mathf.Lerp(animator.GetFloat("Speed"), 1f, 0.2f): Mathf.Lerp(animator.GetFloat("Speed"), 0f, 0.2f));
        animator.stabilizeFeet = stabilizefoot;
    }
}
