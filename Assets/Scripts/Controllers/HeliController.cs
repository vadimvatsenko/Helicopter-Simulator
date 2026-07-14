using System.Collections.Generic;
using Characteristics;
using Engines;
using Old_Input;
using Rigidbodies;
using UnityEngine;
using Weapons;

namespace Controllers
{
    [RequireComponent(typeof(Rigidbody), typeof(InputController))]
    
    public class HeliController : BaseRbController
    {
        [Header("Helicopter Properties")]
        // List Engines // список двигателей
        [SerializeField] private List<HeliEngine> engines = new List<HeliEngine>();
        [Header("Helicopter Rotors")]
        [SerializeField] private HeliRotorController rotorController;
        
        private HeliCharacteristics _characteristics;
        private InputController _input;
        private HeliWeaponController _weaponController;

        public HeliRotorController RotorController
        {
            get => rotorController;
            set => rotorController = value;
        }
        
        protected override void Start()
        {
            base.Start();
            _input = GetComponent<InputController>();
            _characteristics = GetComponent<HeliCharacteristics>();
            _weaponController = GetComponentInChildren<HeliWeaponController>();
        }

        protected override void HandlePhysics()
        { 
            HandleEngines();
            HandleRotors();
            HandleCharacteristics();
            if (_weaponController)
            {
                HandleWeapons();
            }
        }

        protected virtual void HandleWeapons()
        {
            _weaponController.UpdateWeapons(_input);
        }

        protected virtual void HandleRotors()
        {
            if (engines.Count > 0)
            {
                rotorController.UpdateRotors(_input, engines[0].CurrentRpm);
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
                float finalPower = engines[i].CurrentHp;
                //Debug.Log(finalPower);
            }
        }

        public void AddEngine(HeliEngine engine) => engines.Add(engine);
    }
}
