using Helicopter_Game.Scripts.Old_Input;
using UnityEngine;

namespace Helicopter_Game.Scripts.Rotors
{
    public interface IP_IHeliRotor 
    {
        void UpdateRotors(float currentDps, IP_Input_Controller input);
    }
}
