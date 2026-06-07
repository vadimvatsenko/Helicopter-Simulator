using System;
using Helicopter_Game.Scripts.Old_Input;
using Helicopter_Game.Scripts.Rigidbodies;
using UnityEngine;

namespace Helicopter_Game.Scripts.Controllers
{
    [RequireComponent(typeof(Rigidbody), typeof(IP_Input_Controller))]
    
    public class IP_Heli_Controller : IP_Base_RBController
    {
        //[Header("Controller Properties")]
        private IP_Input_Controller input;

        protected override void Start()
        {
            base.Start();
            input = GetComponent<IP_Input_Controller>();
        }

        protected override void HandlePhysics()
        { 
            HandleEngines();
            HandleCharacteristics();
        }

        protected virtual void HandleCharacteristics()
        {
            
        }

        protected virtual void HandleEngines()
        {
           
        }
    }
}
