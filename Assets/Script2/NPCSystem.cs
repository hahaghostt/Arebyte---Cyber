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

                NewDialogue("Hi");
                NewDialogue("Kath3r1ne Dialogue???");
                NewDialogue("WOrk pls");

                pressE.SetActive(false); 
                canva.transform.GetChild(0).gameObject.SetActive(true); 

            }
        
        }

        void NewDialogue(string text)
        {
            GameObject template_clone = Instantiate(d_template, d_template.transform);
            template_clone.transform.SetParent (canva.transform);
            template_clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text; 

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
