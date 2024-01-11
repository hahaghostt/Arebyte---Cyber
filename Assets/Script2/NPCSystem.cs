using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CyberMovementSystem
{
    public class NPCSystem : MonoBehaviour
    {
        public GameObject d_template;
        public GameObject canva;

        public GameObject defaultSprite; // Default character sprite GameObject
        public GameObject happySprite;   // Character sprite for happy dialogue

        public string[] Dialogue;
        public int placement;

        public TMPro.TextMeshProUGUI text2;

        public GameObject pressE;
        public GameObject TASK;
        public GameObject TASK2;

        bool player_detection = false;

        void Start()
        {
            defaultSprite.SetActive(false); // Start with the default sprite turned off
            happySprite.SetActive(false);   // Start with the happy sprite turned off

        }

        void Update()
        {
            if (player_detection && Input.GetKeyDown(KeyCode.E) && !CharacterController2.dialogue)
            {
                canva.SetActive(true);
                CharacterController2.dialogue = true;
                placement = 0;

                pressE.SetActive(false);
                d_template.SetActive(true);
                defaultSprite.SetActive(true); // Turn on the default sprite
                UpdateDialogue();
            }
            else if (player_detection && Input.GetKeyDown(KeyCode.E) && CharacterController2.dialogue)
            {
                placement += 1;
                UpdateDialogue();
            }
        }

        void UpdateDialogue()
        {
            if (placement < Dialogue.Length)
            {
                text2.text = Dialogue[placement];

                // Switching sprites based on specific keywords in the dialogue
                if (Dialogue[placement].Contains("get to it"))
                {
                    defaultSprite.SetActive(false);
                    
                    happySprite.SetActive(true);
                }
                else if (Dialogue[placement].Contains("Here"))
                {
                    defaultSprite.SetActive(false);
                    happySprite.SetActive(true);
                 
                }
                else if (Dialogue[placement].Contains("Hey!"))
                {
                    happySprite.SetActive(false);
                
                    defaultSprite.SetActive(true);
                }
            }
            else
            {
                CharacterController2.dialogue = false;
                d_template.SetActive(false);
                defaultSprite.SetActive(false);
                happySprite.SetActive(false);
            
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "PlayerBody")
            {
                player_detection = true;
                pressE.SetActive(true);
                TASK2.SetActive(false);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            player_detection = false;
            pressE.SetActive(false);
            TASK.SetActive(true);
        }
    }
}