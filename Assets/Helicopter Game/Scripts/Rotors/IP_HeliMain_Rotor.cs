using Helicopter_Game.Scripts.Old_Input;
using UnityEngine;

namespace Helicopter_Game.Scripts.Rotors
{
    public class IP_HeliMain_Rotor : MonoBehaviour, IP_IHeliRotor
    {
        [Header("Main Rotor Properties")] 
        // left rotor // левая и правая лопасть 
        [SerializeField] private Transform lRotor;
        [SerializeField] private Transform rRotor;
        // максимальный угол поворота лопасти
        [SerializeField] private float maxPitch = 35f;
        
        public float CurrentRPMs {get; private set;}
        
        public void UpdateRotors(float dps, IP_Input_Controller input)
        {
            CurrentRPMs = (dps / 360) * 60f;
            transform.Rotate(Vector3.up, dps);

            lRotor.localRotation = Quaternion.Euler(input.StickyCollectiveInput * maxPitch, 0, 0);
            rRotor.localRotation = Quaternion.Euler(-input.StickyCollectiveInput * maxPitch, 0, 0);
            
        }
    }
}
