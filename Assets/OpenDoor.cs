using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberMovementSystem
{
    public class OpenDoor : MonoBehaviour
    {

        private Animator animate;
        private Animator animate2; 
        // Start is called before the first frame update
        void Start()
        {
            animate = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void OnTriggerEnter(Collider other)
        {
            animate.SetTrigger("MoveDoor");
            animate2.SetTrigger("MoveDoor2"); 
        }
    }
}
