using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberMovementSystem
{
    public class PlayerMovementState : IState
    {
        public virtual void Enter()
        {
            Debug.Log("State: " + GetType().Name); 
        }

        public virtual void Exit()
        {
           
        }

        public virtual void HandleInput()
        {
           
        }

        public virtual void Update()
        {

        }

        public virtual void PhysicsUpdate()
        {
           
        }

    }
}
