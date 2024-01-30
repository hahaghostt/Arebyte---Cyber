using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberMovementSystem
{
    public class PlayerSubtitles : MonoBehaviour
    {
        public GameObject ItsHot;
        bool player_detection = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "PlayerBody")
            {
                player_detection = true;
                ItsHot.SetActive(true);
                
            }
        }

        private void OnTriggerExit(Collider other)
        {
            player_detection = false;
            ItsHot.SetActive(false);
            Destroy(gameObject); 

        }
    }
}
