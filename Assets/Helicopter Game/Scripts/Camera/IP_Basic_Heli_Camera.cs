using UnityEngine;

namespace Helicopter_Game.Scripts.Camera
{
    public class IP_Basic_Heli_Camera : IP_Base_HeliCamera
    {
        [Header("Basic Camera Properties")] 
        [SerializeField] private float height = 5f;
        [SerializeField] private float distance = 5f;
        [SerializeField] private float smoothSpeed = 0.35f;

        private void OnEnable() => updateEvent += UpdateCamera;
        private void OnDisable() => updateEvent -= UpdateCamera;
        
        public void UpdateCamera()
        {
            Vector3 flatFwd = rb.transform.forward;
            flatFwd.y  = 0;
            flatFwd.Normalize();
            
            wantedPos = rb.position + (flatFwd * distance) + (Vector3.up * height);
            transform.position = Vector3.SmoothDamp(transform.position, wantedPos, ref refVelocity, smoothSpeed);
            transform.LookAt(lookAtTarget);
        }
    }
}
