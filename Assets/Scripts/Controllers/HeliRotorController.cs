using System.Collections.Generic;
using System.Linq;
using Old_Input;
using Rotors;
using UnityEngine;

namespace Controllers
{
    public class HeliRotorController : MonoBehaviour
    {
        [SerializeField] private bool isArcadeRotor = false;
        [SerializeField] private float maxDps = 3000f;
        private List<IHeliRotor> _heliRotors;

        private void Start()
        {
            _heliRotors = GetComponentsInChildren<IHeliRotor>().ToList();
        }
        
        public void UpdateRotors(InputController input, float currentRPM)
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
