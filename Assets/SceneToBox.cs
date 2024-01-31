using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CyberMovementSystem
{
    public class SceneToBox : MonoBehaviour
    {


        private void OnTriggerEnter(Collider other)
        {
            SceneManager.LoadScene(4);
        }
    }
}
