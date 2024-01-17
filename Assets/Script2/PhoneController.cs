using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CyberMovementSystem
{
    public class PhoneController : MonoBehaviour
    {
        public GameObject[] textBoxes; // Array of GameObjects to be activated
        private int currentTextIndex = 0;    // Index of the currently active object

        public Animator SlideTransition;
        public float SlideSpeed; 

        private void Start()
        {
            textBoxes[currentTextIndex].SetActive(false);
            SlideTransition = GetComponent<Animator>();
        }

        void Update()
        {
            // Check if the "E" key is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (textBoxes != null && textBoxes.Length > 0)
                {
                    
                    currentTextIndex = (currentTextIndex + 1) % textBoxes.Length;

                    textBoxes[currentTextIndex].SetActive(true);

                    if (currentTextIndex == textBoxes.Length - 1)
                    {
                        // All text boxes are displayed, load the next scene
                        LoadNextScene();
                    }
                }
            }
        }

        void LoadNextScene()
        {
            string nextSceneName = "Map";

            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogWarning("Next scene");
            }
        }
    }
}
