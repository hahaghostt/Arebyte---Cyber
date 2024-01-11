using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CyberMovementSystem
{
    public class LookAtItems : MonoBehaviour
    {
        public GameObject ItemE;
        public GameObject Item;
        bool player_detection = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "PlayerBody")
            {
                player_detection = true;
                ItemE.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            player_detection = false;
            ItemE.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if (player_detection && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Work");
                player_detection = true;
                Item.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Item.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}