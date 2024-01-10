using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

namespace CyberMovementSystem
{
    public class NPCSystem : MonoBehaviour
    {
        public GameObject d_template;
        public GameObject canva;

        public string[] Dialogue;
        public int placement;

        public TMPro.TextMeshProUGUI text2; 

        public GameObject pressE; 
        bool player_detection = false;
        // Start is called before the first frame update
        void Update()
        {
            if (player_detection && Input.GetKeyDown(KeyCode.E) && !CharacterController2.dialogue)
            {
                canva.SetActive(true);
                CharacterController2.dialogue = true;
                // print("Dialogue Started");
                placement = 0;

                /*
                for (int i = 0; i < Dialogue.Length; i++)
                {
                    Dialogue[i] = "";
                }

                NewDialogue("Hi");
                NewDialogue("Kath3r1ne Dialogue???");
                NewDialogue("WOrk pls");
                */

                placement = 0;
                text2.text = Dialogue[placement];

                pressE.SetActive(false);
                d_template.SetActive(true);
                //  newUI.SetActive(true); 
                //   canva.transform.GetChild(0).gameObject.SetActive(true); 


            }

            else if (player_detection && Input.GetKeyDown(KeyCode.E) && CharacterController2.dialogue)
            {
                placement += 1;
                if (Dialogue[placement] != "")
                {
                    text2.text = Dialogue[placement]; 
                }

                else
                {
                    CharacterController2.dialogue = false;
                    d_template.SetActive(false); 
                }
               

            }
        
        }

        void NewDialogue(string text)
        {
            Dialogue[placement] = text;
            placement += 1; 
            /* GameObject template_clone = Instantiate(d_template, d_template.transform);
            template_clone.transform.SetParent (canva.transform);
            template_clone.transform.localPosition = Vector3.zero;
            template_clone.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = text;
            newUI = template_clone; */ 

        }


        private void OnTriggerEnter(Collider other)
        {
            if(other.name == "PlayerBody")
            {
                player_detection = true;
                pressE.SetActive(true); 
            }
        }

        private void OnTriggerExit(Collider other)
        {
            player_detection = false;
            pressE.SetActive(false); 
        }

    }
}
