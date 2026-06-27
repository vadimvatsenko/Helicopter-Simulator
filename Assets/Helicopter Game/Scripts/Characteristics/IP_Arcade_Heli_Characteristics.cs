using Helicopter_Game.Scripts.Old_Input;
using UnityEngine;

namespace Helicopter_Game.Scripts.Characteristics
{
    public class IP_Arcade_Heli_Characteristics : IP_Heli_Characteristics
    {
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
            base.HandleCyclic(rb, input);
        }

        protected override void HandlePedals(Rigidbody rb, IP_Input_Controller input)
        {
            base.HandlePedals(rb, input);
        }
    }
}
