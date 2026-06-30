using Old_Input;
using UnityEngine;

namespace Characteristics
{
    public class IP_Arcade_Heli_Characteristics : IP_Heli_Characteristics
    {
        [Header("Arcade Properties")]
        [SerializeField] private float blankAngle = 35f;
        [SerializeField] private float blankSpeed = 1.5f;
        
        private float _yRot = 0f;
        private float _xRot = 0f;
        private float _zRot = 0f;

        private Quaternion _finalRot = Quaternion.identity;
        
        protected override void HandleLift(Rigidbody rb, IP_Input_Controller input)
        {
            // зависаю в воздухе
            Vector3 liftForce = Vector3.up * (Physics.gravity.magnitude * rb.mass);
            rb.AddForce(liftForce, ForceMode.Force);
            //
            
            rb.AddForce(Vector3.up * (input.ThrottleInput * maxLiftForce), ForceMode.Acceleration);
        }
        
        protected override void HandleCyclic(Rigidbody rb, IP_Input_Controller input)
        {
            Vector3 fwdDir = input.CyclicInput.y * FlatFwd;
            Vector3 rightDir = input.CyclicInput.x * FlatRight;
            Vector3 finalDir = (fwdDir + rightDir).normalized;
            
            rb.AddForce(finalDir * cyclicForce, ForceMode.Acceleration);

            _xRot = input.CyclicInput.y * blankAngle;
            _zRot = -input.CyclicInput.x * blankAngle;
        }

        protected override void HandlePedals(Rigidbody rb, IP_Input_Controller input)
        {
            _yRot += input.PedalInput * tailForce;
        }

        protected override void AutoLevel(Rigidbody rb)
        {
            Quaternion wantedRot = Quaternion.Euler(_xRot, _yRot, _zRot);
            _finalRot = Quaternion.Slerp(_finalRot, wantedRot, Time.fixedDeltaTime * blankSpeed);
            rb.MoveRotation(_finalRot);
        }
    }
}
