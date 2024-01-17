using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CyberMovementSystem
{
    public class NPCSystem : MonoBehaviour
    {
        public GameObject d_template;
        public GameObject canva;

        public GameObject defaultSprite;
        public GameObject happySprite;
        public GameObject People;

        public GameObject objectToShow;
        public float delayInSeconds = 2f;
        public GameObject KillObject; 

        public string[] Dialogue;
        public int placement;

        public TMPro.TextMeshProUGUI text2;

        public GameObject pressE;
        public GameObject TASK;
        public GameObject TASK2;

        public string[] Name;
        public TMPro.TextMeshProUGUI text3;
        public int Name2;

        public bool Options;
        public GameObject OptionsDialogue;

        bool player_detection = false;

        void Start()
        {
            defaultSprite.SetActive(false);
            People.SetActive(false);
            happySprite.SetActive(false);
            Options = false;
            KillObject.SetActive(true); 
            Invoke("ShowObject", delayInSeconds);
        }

        void Update()
        {
            if (player_detection && Input.GetKeyDown(KeyCode.E) && !CharacterController2.Dialogue)
            {
                canva.SetActive(true);
                CharacterController2.Dialogue = true;
                placement = 0;
                string[] name = new string[Name.Length];
                text3.text = Name[Name2];

                pressE.SetActive(false);
                d_template.SetActive(true);
                defaultSprite.SetActive(true);
                UpdateDialogue();
            }
            else if (player_detection && Input.GetKeyDown(KeyCode.E) && CharacterController2.Dialogue)
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
                else if (Dialogue[placement].Contains("Yeah, I’d really appreciate that."))
                {
                    defaultSprite.SetActive(true);
                    happySprite.SetActive(false);
                    OptionsDialogue.SetActive(true);
                }

                else if (Dialogue[placement].Contains("You can go back to the Vtuber headquaters and ask for who sells them"))
                {
                    People.SetActive(true);
                    if (objectToShow != null)
                    {
                        objectToShow.SetActive(true);
                    }
                }

                else if (Dialogue[placement].Contains("get to it! "))
                {
                    KillObject.SetActive(false); 
                }
            }
            else
            {
                CharacterController2.Dialogue = false;
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
