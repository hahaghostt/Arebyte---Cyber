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

        public GameObject defaultSprite; 
        public GameObject happySprite;   

        public string[] Dialogue;
        public int placement;

        public TMPro.TextMeshProUGUI text2;

        public GameObject pressE;
        public GameObject TASK;
        public GameObject TASK2;

        public string[] Name; 

        bool player_detection = false;

        void Start()
        {
            defaultSprite.SetActive(false);
            happySprite.SetActive(false);   

        }

        void Update()
        {
            if (player_detection && Input.GetKeyDown(KeyCode.E) && !CharacterController2.dialogue)
            {
                canva.SetActive(true);
                CharacterController2.dialogue = true;
                placement = 0;
                string[] name = new string[Name.Length];

                pressE.SetActive(false);
                d_template.SetActive(true);
                defaultSprite.SetActive(true); 
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