using System.Collections.Generic;
using System.Linq;
using Helicopter_Game.Scripts.Old_Input;
using Helicopter_Game.Scripts.Rotors;
using UnityEngine;

namespace Helicopter_Game.Scripts.Controllers
{
    public class IP_HeliRotor_Controller : MonoBehaviour
    {
        [SerializeField] private bool isArcadeRotor = false;
        [SerializeField] private float maxDps = 3000f;
        private List<IP_IHeliRotor> _heliRotors;

        private void Start()
        {
            _heliRotors = GetComponentsInChildren<IP_IHeliRotor>().ToList();
        }
        
        public void UpdateRotors(IP_Input_Controller input, float currentRPM)
        {
            float dps = ((currentRPM * 360f) / 60f);
            dps = Mathf.Clamp(dps, 0f, maxDps);

            if (isArcadeRotor)
            {
                dps = 4000;
            }
            
            foreach (var heliRotor in _heliRotors)
            {
                heliRotor.UpdateRotors(dps, input);
            }
        }
    }
}
