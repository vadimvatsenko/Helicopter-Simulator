using Helicopter_Game.Scripts.Old_Input;
using Helicopter_Game.Scripts.Rotors;
using UnityEngine;

public class IP_HeliTail_Rotor : MonoBehaviour, IP_IHeliRotor
{
    [SerializeField] private float rotationSpeedModifer = 1.5f;
    public void UpdateRotors(float currentDps,  IP_Input_Controller input)
    {
        transform.Rotate(Vector3.right, currentDps * rotationSpeedModifer);
    }
}
