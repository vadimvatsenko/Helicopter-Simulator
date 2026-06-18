using UnityEngine;

namespace Helicopter_Game.Scripts.Old_Input
{
    public class IP_XboxHeli_Input : IP_KeyboardHeli_Input
    {
        private const string XBOX_CYCLIC_HORIZONTAL = "XBoxCyclicHorizontal";
        private const string XBOX_CYCLIC_VERTICAL = "XBoxCyclicVertical";
        private const string XBOX_COLLECTIVE = "XBoxCollective";
        private const string XBOX_PEDAL = "XBoxPedal";
        private const string XBOX_THROTTLE_UP = "XBoxThrottleUp";
        private const string XBOX_THROTTLE_DOWN = "XBoxThrottleDown";
        private const string XBOX_CAMERA_BUTTON = "XBoxCamBtn";
        
        protected override void HandleThrottle()
        {
            RawThrottleInput = Input.GetAxis(XBOX_THROTTLE_UP) + -Input.GetAxis(XBOX_THROTTLE_DOWN);
        }

        protected override void HandleCollective()
        {
            CollectiveInput = Input.GetAxis(XBOX_COLLECTIVE);
        }

        protected override void HandleCyclic()
        {
            float x = Input.GetAxis(XBOX_CYCLIC_HORIZONTAL);
            float y = Input.GetAxis(XBOX_CYCLIC_VERTICAL);
            CyclicInput = new Vector2(x, y);
        }
         
        protected override void HandlePedal()
        {
            PedalInput = Input.GetAxis(XBOX_PEDAL);
        }

        protected override void HandleCamBtn()
        {
            CamInput = Input.GetButtonDown(XBOX_CAMERA_BUTTON);
        }
    }
}
