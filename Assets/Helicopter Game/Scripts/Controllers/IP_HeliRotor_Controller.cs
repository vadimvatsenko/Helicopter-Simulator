using System.Collections.Generic;
using System.Linq;
using Helicopter_Game.Scripts.Old_Input;
using Helicopter_Game.Scripts.Rotors;
using UnityEngine;

namespace Helicopter_Game.Scripts.Controllers
{
    public class IP_HeliRotor_Controller : MonoBehaviour
    {
        private List<IP_IHeliRotor> heliRotors;

        private void Start()
        {
            heliRotors = GetComponentsInChildren<IP_IHeliRotor>().ToList();
            Debug.Log(heliRotors.Count);
        }
        
        public void UpdateRotors(IP_Input_Controller input, float currentRPM)
        {
            Debug.Log(currentRPM);
            // Degrees per second calculation
            float dps = ((currentRPM * 360f) / 60f) * Time.deltaTime;
            
            foreach (var heliRotor in heliRotors)
            {
                heliRotor.UpdateRotors(dps, input);
            }
        }
    }
}
