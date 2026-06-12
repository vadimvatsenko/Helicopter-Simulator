using Helicopter_Game.Scripts.Old_Input;
using Helicopter_Game.Scripts.Rigidbodies;
using Helicopter_Game.Scripts.Rotors;
using UnityEngine;

namespace Helicopter_Game.Scripts.Characteristics
{
    public class IP_Heli_Characteristics : MonoBehaviour
    {
        [Header("Lift Properties")] 
        [SerializeField] private float maxLiftForce = 100f;

        private IP_HeliMain_Rotor mainRotor;

        private void Start()
        {
            mainRotor = GetComponentInChildren<IP_HeliMain_Rotor>();
        }
        
        public void UpdateCharacteristics(Rigidbody rb, IP_Input_Controller input)
        {
            HandleLift(rb, input);
            HandleCyclic(rb, input);
            HandlePedals(rb, input);
        }
        
        protected virtual void HandleLift(Rigidbody rb, IP_Input_Controller input)
        {
            Vector3 liftForce = transform.up * ((Physics.gravity.magnitude + maxLiftForce) * rb.mass);
            float normalizedRPMs = mainRotor.CurrentRPMs / 500f;
            rb.AddForce(liftForce * (Mathf.Pow(normalizedRPMs, 2f) * Mathf.Pow(input.StickyCollectiveInput, 2f)), ForceMode.Force);
        }
        protected virtual void HandleCyclic(Rigidbody rb, IP_Input_Controller input)
        {
            
        }

        protected virtual void HandlePedals(Rigidbody rb, IP_Input_Controller input)
        {
            
        }
    }
}
