using Helicopter_Game.Scripts.Old_Input;
using UnityEngine;

namespace Helicopter_Game.Scripts.Characteristics
{
    public class IP_Arcade_Heli_Characteructics : IP_Heli_Characteristics
    {
        protected override void HandleLift(Rigidbody rb, IP_Input_Controller input)
        {
            base.HandleLift(rb, input);
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
