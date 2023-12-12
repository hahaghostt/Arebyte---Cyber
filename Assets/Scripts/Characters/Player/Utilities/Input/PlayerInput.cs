using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberMovementSystem
{
    public class PlayerInput : MonoBehaviour
    {
        public PlayerInputActions InputActions {  get; private set; }

        public PlayerInputActions.PlayerActions PlayerActions {  get; private set; }

        private void Awake()
        {
            InputActions = new PlayerInputActions();

            PlayerActions = InputActions.Player; 
        }

        private void OnEnable()
        {
            InputActions.Enable(); 

        }

        public void OnDisable()
        {
            InputActions.Disable();
        }
    }
}
