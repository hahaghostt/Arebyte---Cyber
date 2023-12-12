using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberMovementSystem
{
    public abstract class StateMachine
    {
        protected IState currentState;
        
        public void ChangeState(IState newState)
        {
            currentState?.Exit(); 

            currentState = newState;

            currentState.Enter(); 
        }

        public void HandleInput()
        {
            currentState?.HandleInput(); 
        }

        public void Update()
        {
            currentState?.HandleInput();
        }

        public void PhysicsUpdate()
        {
            currentState?.HandleInput();
        }
    }
}
