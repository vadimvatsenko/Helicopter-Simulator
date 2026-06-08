using System;
using System.Collections.Generic;
using Helicopter_Game.Scripts.Engines;
using Helicopter_Game.Scripts.Old_Input;
using Helicopter_Game.Scripts.Rigidbodies;
using UnityEngine;

namespace Helicopter_Game.Scripts.Controllers
{
    [RequireComponent(typeof(Rigidbody), typeof(IP_Input_Controller))]
    
    public class IP_Heli_Controller : IP_Base_RBController
    {
        [Header("Helicopter Properties")]
        [SerializeField] private List<IP_Heli_Engine> engines = new List<IP_Heli_Engine>();
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
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i].UpdateEngine(input.StickyThrottle);
                float finalPower = engines[i].CurrentHP;
            }
        }
    }
}
