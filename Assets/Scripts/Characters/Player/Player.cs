
using UnityEngine;

namespace CyberMovementSystem
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        public PlayerInput Input { get; private set; }
        private PlayerMovementStateMachine movementStateMachine;



        private void Awake()
        {
            movementStateMachine = new PlayerMovementStateMachine(); 

        }

        private void Start()
        {
            Input = GetComponent<PlayerInput>();
            movementStateMachine.ChangeState(movementStateMachine.IdlingState); 
        }

        private void Update()
        {
            movementStateMachine.HandleInput();

            movementStateMachine.Update(); 
        }

        private void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate(); 
        }

    }
}
