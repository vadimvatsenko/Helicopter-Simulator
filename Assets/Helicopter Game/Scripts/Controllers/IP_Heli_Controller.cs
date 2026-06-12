using System;
using System.Collections.Generic;
using Helicopter_Game.Scripts.Characteristics;
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
        // List Engines // список двигателей
        [SerializeField] private List<IP_Heli_Engine> engines = new List<IP_Heli_Engine>();
        [Header("Helicopter Rotors")]
        [SerializeField] private IP_HeliRotor_Controller rotorController;
        
        private IP_Heli_Characteristics characteristics;
        private IP_Input_Controller input;
        
        protected override void Start()
        {
            base.Start();
            input = GetComponent<IP_Input_Controller>();
            characteristics = GetComponent<IP_Heli_Characteristics>();
            Debug.Log(characteristics.name);
        }

        protected override void HandlePhysics()
        { 
            HandleEngines();
            HandleRotors();
            HandleCharacteristics();
        }

        protected virtual void HandleRotors()
        {
            if (engines.Count > 0)
            {
                rotorController.UpdateRotors(input, engines[0].CurrentRPM);
            }
        }

        protected virtual void HandleCharacteristics()
        {
            characteristics.UpdateCharacteristics(rb, input);
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
