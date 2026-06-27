using Helicopter_Game.Scripts.Old_Input;
using UnityEngine;

namespace Helicopter_Game.Scripts.Characteristics
{
    public class IP_Arcade_Heli_Characteristics : IP_Heli_Characteristics
    {
        [Header("Arcade Properties")]
        [SerializeField] private float blankAngle = 35f;
        [SerializeField] private float blankSpeed = 1.5f;
        
        private float yRot = 0f;
        private float xRot = 0f;
        private float zRot = 0f;

        private Quaternion finalRot = Quaternion.identity;
        
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
            Vector3 fwdDir = input.CyclicInput.y * flatFwd;
            Vector3 rightDir = input.CyclicInput.x * flatRight;
            Vector3 finalDir = (fwdDir + rightDir).normalized;
            
            rb.AddForce(finalDir * cyclicForce, ForceMode.Acceleration);

            xRot = input.CyclicInput.y * blankAngle;
            zRot = -input.CyclicInput.x * blankAngle;
        }

        protected override void HandlePedals(Rigidbody rb, IP_Input_Controller input)
        {
            yRot += input.PedalInput * tailForce;
        }

        protected override void AutoLevel(Rigidbody rb)
        {
            Quaternion wantedRot = Quaternion.Euler(xRot, yRot, zRot);
            finalRot = Quaternion.Slerp(finalRot, wantedRot, Time.fixedDeltaTime * blankSpeed);
            rb.MoveRotation(finalRot);
        }
    }
}
