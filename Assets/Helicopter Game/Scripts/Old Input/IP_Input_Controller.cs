using UnityEngine;
using UnityEngine.UI;

namespace Helicopter_Game.Scripts.Old_Input
{
    
    [RequireComponent(typeof(IP_KeyboardHeli_Input), typeof(IP_XboxHeli_Input), typeof(IP_MobileHeli_Input))]
public class IP_Input_Controller : MonoBehaviour
    {
        [Header("Input Components")]
        [SerializeField] private InputType inputType = InputType.KeyBoard;
        private IP_KeyboardHeli_Input keyboardInput;
        private IP_XboxHeli_Input xboxInput;
        private IP_MobileHeli_Input mobileInput;

        private void Start()
        {
            keyboardInput = GetComponent<IP_KeyboardHeli_Input>();
            xboxInput = GetComponent<IP_XboxHeli_Input>();
            mobileInput = GetComponent<IP_MobileHeli_Input>();
            SetInputType(inputType);
        }
        
        private void SetInputType(InputType type)
        {
            switch (type)
            {
                case InputType.KeyBoard:
                    inputType = InputType.KeyBoard;
                    keyboardInput.enabled = true;
                    xboxInput.enabled = false;
                    mobileInput.enabled = false;
                    break;
                case InputType.XBox:
                    inputType = InputType.XBox;
                    xboxInput.enabled = true;
                    keyboardInput.enabled = false;
                    mobileInput.enabled = false;
                    break;
                case InputType.Mobile:
                    inputType = InputType.Mobile;
                    mobileInput.enabled = true;
                    xboxInput.enabled = false;
                    keyboardInput.enabled = false;
                    break;
                default:
                    break;
            }
        }
    }
}
