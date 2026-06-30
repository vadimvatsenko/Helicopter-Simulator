using Old_Input;
using UnityEngine;

namespace Rotors
{
    public class IP_HeliTail_Rotor : MonoBehaviour, IP_IHeliRotor
    {
        [SerializeField] private float rotationSpeedModifer = 1.5f;
        [SerializeField] private Transform lRotor;
        [SerializeField] private Transform rRotor;
        // максимальный угол поворота лопасти
        [SerializeField] private float maxPitch = 45f;
    
    
        public void UpdateRotors(float dps,  IP_Input_Controller input)
        {
            transform.Rotate(Vector3.right, dps * rotationSpeedModifer);

            if (lRotor && rRotor)
            {
                lRotor.localRotation = Quaternion.Euler(0, input.PedalInput * maxPitch, 0);
                rRotor.localRotation = Quaternion.Euler(0, -input.PedalInput * maxPitch, 0);
            }
        }
    }
}
