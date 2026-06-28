using System.Collections.Generic;
using System.Linq;
using Helicopter_Game.Scripts.Old_Input;
using Helicopter_Game.Scripts.Rotors;
using UnityEngine;

namespace Helicopter_Game.Scripts.Controllers
{
    public class IP_HeliRotor_Controller : MonoBehaviour
    {
        private List<IP_IHeliRotor> _heliRotors;

        private void Start()
        {
            _heliRotors = GetComponentsInChildren<IP_IHeliRotor>().ToList();
        }
        
        public void UpdateRotors(IP_Input_Controller input, float currentRPM)
        {
            // Degrees per second calculation
            //float dps = ((currentRPM * 360f) / 60f) * Time.fixedDeltaTime;
            
            foreach (var heliRotor in _heliRotors)
            {
                //heliRotor.UpdateRotors(dps, input);
                heliRotor.UpdateRotors(currentRPM, input);
            }
        }
    }
}
