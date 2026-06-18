using UnityEngine;

namespace Helicopter_Game.Scripts.Camera
{
    public class IP_CockPit_HeliCamera : IP_Base_HeliCamera, IP_IHeliCamera
    {
        [Header("Cockpit Camera Properties")]
        [SerializeField] Transform cockpitPosition;
        [SerializeField] private Vector3 offset = Vector3.zero;
        [SerializeField] private float fov = 70f;
        
        private Vector3 startOffset;
        
        private void OnEnable()
        {
            updateEvent += UpdateCamera;
        }

        private void OnDisable()
        {
            updateEvent -= UpdateCamera;
        }

        public void UpdateCamera()
        {
            transform.position = cockpitPosition.position + startOffset;
            transform.LookAt(lookAtTarget);
        }
    }
}
