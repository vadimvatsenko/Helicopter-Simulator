using System;
using UnityEngine;

namespace Helicopter_Game.Scripts.Old_Input
{
    public class IP_BaseHeli_Input : MonoBehaviour
    {
        [Header("Base Input Properties")]
        protected float verticalInput = 0f;
        protected float horizontalInput = 0f;

        public void Update()
        {
            HandleInput();
            
        }

        protected virtual void HandleInput()
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");
        }
        
        
    }
}
