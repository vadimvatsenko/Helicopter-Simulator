using UnityEngine;
using UnityEngine.UI;

namespace Helicopter_Game.Scripts.Old_Input
{
    public class IP_Input_Controller : MonoBehaviour
    {
        [Header("Input Components")]
        [SerializeField] private InputType inputType = InputType.KeyBoard;
        [SerializeField] private IP_KeyboardHeli_Input keyboardInput;
        [SerializeField] private IP_XboxHeli_Input xboxInput;
        [SerializeField] private IP_MobileHeli_Input mobileInput;

        private void Start()
        {
            SetInputType(inputType);
        }
        
        private void SetInputType(InputType type)
        {
            switch (type)
            {
                case InputType.KeyBoard:
                    keyboardInput.enabled = true;
                    xboxInput.enabled = false;
                    mobileInput.enabled = false;
                    break;
                case InputType.XBox:
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
