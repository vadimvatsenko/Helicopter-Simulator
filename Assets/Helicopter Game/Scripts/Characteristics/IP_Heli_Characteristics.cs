using Helicopter_Game.Scripts.Old_Input;
using Helicopter_Game.Scripts.Rotors;
using UnityEngine;

namespace Helicopter_Game.Scripts.Characteristics
{
    public class IP_Heli_Characteristics : MonoBehaviour
    {
        [Header("Lift Properties")] 
        [SerializeField] private float maxLiftForce = 100f;
        [Space]
        [Header("Tail Rotor Properties")]
        [SerializeField] private float tailForce = 2f;
        [Space]
        [Header("Cyclic Properties")]
        [SerializeField] private float cyclicForce = 2f;
        
        private IP_HeliMain_Rotor mainRotor;
        private IP_HeliTail_Rotor tailRotor;

        private void Start()
        {
            mainRotor = GetComponentInChildren<IP_HeliMain_Rotor>();
            tailRotor = GetComponentInChildren<IP_HeliTail_Rotor>();
        }
        
        public void UpdateCharacteristics(Rigidbody rb, IP_Input_Controller input)
        {
            HandleLift(rb, input);
            HandleCyclic(rb, input);
            HandlePedals(rb, input);
        }
        
        // поднятие вверх
        protected virtual void HandleLift(Rigidbody rb, IP_Input_Controller input)
        {
            /*Vector3 liftForce = transform.up * ((Physics.gravity.magnitude + maxLiftForce) * rb.mass);
            // почему 450, потому что CurrentRPMs 2700
            float normalizedRPMs = mainRotor.CurrentRPMs / 450f;
            rb.AddForce(liftForce * (Mathf.Pow(normalizedRPMs, 2f) * Mathf.Pow(input.StickyCollectiveInput, 2f)), ForceMode.Force);*/
            
            //левитация
            Vector3 liftForce = transform.up * Physics.gravity.magnitude * rb.mass;
            rb.AddForce(liftForce, ForceMode.Force);
        }
        protected virtual void HandleCyclic(Rigidbody rb, IP_Input_Controller input)
        {
            float cyclicZForce = input.CyclicInput.x  * cyclicForce;
            rb.AddRelativeTorque(Vector3.forward * cyclicZForce, ForceMode.Acceleration);
            
            float cyclicXForce = input.CyclicInput.y * cyclicForce;
            rb.AddRelativeTorque(Vector3.right * cyclicXForce, ForceMode.Acceleration);
        }

        protected virtual void HandlePedals(Rigidbody rb, IP_Input_Controller input)
        {
            Debug.Log(input.PedalInput);
            rb.AddTorque(Vector3.up * (input.PedalInput * tailForce), ForceMode.Acceleration);
        }
    }
}
