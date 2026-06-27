using Helicopter_Game.Scripts.Old_Input;
using UnityEngine;

namespace Helicopter_Game.Scripts.Characteristics
{
    public class IP_Arcade_Heli_Characteristics : IP_Heli_Characteristics
    {
        private float yRot = 0f;
        private float xRot = 0f;
        private float zRot = 0f;
        
        protected override void HandleLift(Rigidbody rb, IP_Input_Controller input)
        {
            // зависаю в воздухе
            Vector3 liftForce = transform.up * (Physics.gravity.magnitude * rb.mass);
            rb.AddForce(liftForce, ForceMode.Force);
            //
            
            rb.AddForce(Vector3.up * input.ThrottleInput * maxLiftForce, ForceMode.Acceleration);
        }
        
        protected override void HandleCyclic(Rigidbody rb, IP_Input_Controller input)
        {
            Vector3 fwdDir = input.CyclicInput.y * flatFwd;
            Vector3 rightDir = input.CyclicInput.x * flatRight;
            Vector3 finalDir = (fwdDir + rightDir).normalized;
            
            rb.AddForce(finalDir * cyclicForce, ForceMode.Acceleration);
        }

        protected override void HandlePedals(Rigidbody rb, IP_Input_Controller input)
        {
            yRot += input.PedalInput * tailForce;
            
            Quaternion wantedRot = Quaternion.Euler(0f, yRot, 0f);
            rb.MoveRotation(wantedRot);
        }
    }
}
