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
        
        private IP_Heli_Characteristics _characteristics;
        private IP_Input_Controller _input;

        public IP_HeliRotor_Controller RotorController
        {
            get => rotorController;
            set => rotorController = value;
        }
        
        protected override void Start()
        {
            base.Start();
            _input = GetComponent<IP_Input_Controller>();
            _characteristics = GetComponent<IP_Heli_Characteristics>();
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
                rotorController.UpdateRotors(_input, engines[0].CurrentRPM);
            }
        }

        protected virtual void HandleCharacteristics()
        {
            _characteristics.UpdateCharacteristics(Rb, _input);
        }

        protected virtual void HandleEngines()
        {
            for (int i = 0; i < engines.Count; i++)
            {
                engines[i].UpdateEngine(_input.StickyThrottle);
                float finalPower = engines[i].CurrentHP;
                //Debug.Log(finalPower);
            }
        }

        public void AddEngine(IP_Heli_Engine engine) => engines.Add(engine);
    }
}
