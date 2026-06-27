using Helicopter_Game.Scripts.Old_Input;
using Helicopter_Game.Scripts.Rotors;
using UnityEngine;

namespace Helicopter_Game.Scripts.Characteristics
{
    public class IP_Heli_Characteristics : MonoBehaviour
    {
        [Header("Lift Properties")] 
        [SerializeField] protected float maxLiftForce = 100f;
        [Space(10)]
        [Header("Tail Rotor Properties")]
        [SerializeField] protected float tailForce = 2f;
        [Space(10)]
        [Header("Cyclic Properties")]
        [SerializeField] protected float cyclicForce = 2f;
        [SerializeField] private float cyclicForceMultiplier = 1000f;
        [Space(10)]
        [Header("Auto Level Properties")]
        [SerializeField] float autoLevelForce = 2f;
        
        private IP_HeliMain_Rotor mainRotor;
        private IP_HeliTail_Rotor tailRotor;

        protected Vector3 flatFwd;
        protected float forwardDot;
        protected Vector3 flatRight;
        protected float rightDot;

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

            CalculateAngles();
            AutoLevel(rb);
        }
        
        // поднятие вверх
        protected virtual void HandleLift(Rigidbody rb, IP_Input_Controller input)
        {
            Vector3 liftForce = transform.up * ((Physics.gravity.magnitude + maxLiftForce) * rb.mass);
            // почему 450, потому что CurrentRPMs 2700
            float normalizedRPMs = mainRotor.CurrentRPMs / 450f;
            rb.AddForce(liftForce * (Mathf.Pow(normalizedRPMs, 2f) * Mathf.Pow(input.StickyCollectiveInput, 2f)), ForceMode.Force);
            
            /*//левитация
            Vector3 liftForce = transform.up * Physics.gravity.magnitude * rb.mass;
            rb.AddForce(liftForce, ForceMode.Force);*/
        }
        protected virtual void HandleCyclic(Rigidbody rb, IP_Input_Controller input)
        {
            float cyclicZForce = input.CyclicInput.x  * cyclicForce;
            rb.AddRelativeTorque(Vector3.forward * cyclicZForce, ForceMode.Acceleration);
            
            float cyclicXForce = input.CyclicInput.y * cyclicForce;
            rb.AddRelativeTorque(Vector3.right * cyclicXForce, ForceMode.Acceleration);
            
            Vector3 forwardVec = flatFwd * forwardDot;
            Vector3 rightVec = flatRight * rightDot;
            Vector3 finalCyclicDir 
                = Vector3.ClampMagnitude(forwardVec + rightVec, 1f) * (cyclicForce * cyclicForceMultiplier);
            //Debug.DrawRay(transform.position, finalCyclicDir, Color.green);
            rb.AddForce(finalCyclicDir, ForceMode.Force);
        }

        protected virtual void HandlePedals(Rigidbody rb, IP_Input_Controller input)
        {
            rb.AddTorque(Vector3.up * (input.PedalInput * tailForce), ForceMode.Acceleration);
        }
        
        protected virtual void AutoLevel(Rigidbody rb)
        {
            float rightForce = -forwardDot * autoLevelForce;
            float forwardForce = rightDot * autoLevelForce;
            
            rb.AddRelativeTorque(Vector3.right * rightForce, ForceMode.Acceleration);
            rb.AddRelativeTorque(Vector3.forward * forwardForce, ForceMode.Acceleration);
        }

        protected virtual void CalculateAngles()
        {
            flatFwd = transform.forward;
            flatFwd.y = 0f;
            flatFwd.Normalize();
            
            flatRight = transform.right;
            flatRight.y = 0f;
            flatRight.Normalize();
            
            // Calculate angle
            forwardDot = Vector3.Dot(transform.up, flatFwd);
            rightDot = Vector3.Dot(transform.up, flatRight);
        }
    }
}
