using System;
using UnityEngine;

namespace Helicopter_Game.Scripts.Old_Input
{
    public class IP_BaseHeli_Input : MonoBehaviour
    {
        [Header("Base Input Properties")]
        [SerializeField] private float verticalInput = 0f;
        [SerializeField] private float horizontalInput = 0f;

        public void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");
        }
    }
}
