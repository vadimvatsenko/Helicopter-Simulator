using System;
using UnityEngine;

namespace Helicopter_Game.Scripts.Old_Input
{
    public class IP_BaseHeli_Input : MonoBehaviour
    {
        [Header("Base Input Properties")]
        protected float VerticalInput = 0f;
        protected float HorizontalInput = 0f;

        public void Update()
        {
            HandleInput();
        }

        protected virtual void HandleInput()
        {
            VerticalInput = Input.GetAxis("Vertical");
            HorizontalInput = Input.GetAxis("Horizontal");
        }
    }
}
