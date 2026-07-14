using Old_Input;
using Rotors;
using UnityEngine;

namespace Characteristics
{
    public class HeliCharacteristics : MonoBehaviour
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
        
        private HeliMainRotor _mainRotor;
        private HeliTailRotor _tailRotor;

        protected Vector3 FlatFwd;
        protected float ForwardDot;
        protected Vector3 FlatRight;
        protected float RightDot;

        private void Start()
        {
            _mainRotor = GetComponentInChildren<HeliMainRotor>();
            _tailRotor = GetComponentInChildren<HeliTailRotor>();
        }
        
        public void UpdateCharacteristics(Rigidbody rb, InputController input)
        {
            HandleLift(rb, input);
            HandleCyclic(rb, input);
            HandlePedals(rb, input);

            CalculateAngles();
            AutoLevel(rb);
        }
        
        // поднятие вверх
        protected virtual void HandleLift(Rigidbody rb, InputController input)
        {
            Vector3 liftForce = transform.up * ((Physics.gravity.magnitude + maxLiftForce) * rb.mass);
            // почему 450, потому что CurrentRPMs 2700
            float normalizedRPMs = _mainRotor.CurrentRPMs / 450f;
            rb.AddForce(liftForce * (Mathf.Pow(normalizedRPMs, 2f) * Mathf.Pow(input.StickyCollectiveInput, 2f)), ForceMode.Force);
            
            /*//левитация
            Vector3 liftForce = transform.up * Physics.gravity.magnitude * rb.mass;
            rb.AddForce(liftForce, ForceMode.Force);*/
        }
        protected virtual void HandleCyclic(Rigidbody rb, InputController input)
        {
            float cyclicZForce = input.CyclicInput.x  * cyclicForce;
            rb.AddRelativeTorque(Vector3.forward * cyclicZForce, ForceMode.Acceleration);
            
            float cyclicXForce = input.CyclicInput.y * cyclicForce;
            rb.AddRelativeTorque(Vector3.right * cyclicXForce, ForceMode.Acceleration);
            
            Vector3 forwardVec = FlatFwd * ForwardDot;
            Vector3 rightVec = FlatRight * RightDot;
            Vector3 finalCyclicDir 
                = Vector3.ClampMagnitude(forwardVec + rightVec, 1f) * (cyclicForce * cyclicForceMultiplier);
            //Debug.DrawRay(transform.position, finalCyclicDir, Color.green);
            rb.AddForce(finalCyclicDir, ForceMode.Force);
        }

        protected virtual void HandlePedals(Rigidbody rb, InputController input)
        {
            rb.AddTorque(Vector3.up * (input.PedalInput * tailForce), ForceMode.Acceleration);
        }
        
        protected virtual void AutoLevel(Rigidbody rb)
        {
            float rightForce = -ForwardDot * autoLevelForce;
            float forwardForce = RightDot * autoLevelForce;
            
            rb.AddRelativeTorque(Vector3.right * rightForce, ForceMode.Acceleration);
            rb.AddRelativeTorque(Vector3.forward * forwardForce, ForceMode.Acceleration);
        }

        protected virtual void CalculateAngles()
        {
            FlatFwd = transform.forward;
            FlatFwd.y = 0f;
            FlatFwd.Normalize();
            
            FlatRight = transform.right;
            FlatRight.y = 0f;
            FlatRight.Normalize();
            
            // Calculate angle
            ForwardDot = Vector3.Dot(transform.up, FlatFwd);
            RightDot = Vector3.Dot(transform.up, FlatRight);
        }
    }
}
