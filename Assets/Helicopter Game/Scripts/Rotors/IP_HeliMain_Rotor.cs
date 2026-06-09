using Helicopter_Game.Scripts.Old_Input;
using UnityEngine;

namespace Helicopter_Game.Scripts.Rotors
{
    public class IP_HeliMain_Rotor : MonoBehaviour, IP_IHeliRotor
    {
        [Header("Main Rotor Properties")] 
        // left rotor
        [SerializeField] private Transform lRotor;
        [SerializeField] private Transform rRotor;
        
        public void UpdateRotors(float currentDps, IP_Input_Controller input)
        {
            transform.Rotate(Vector3.up, currentDps);
        }
    }
}
