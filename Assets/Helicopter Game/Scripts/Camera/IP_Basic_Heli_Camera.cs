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
        
        protected virtual void UpdateCamera()
        {
            wantedPos = rb.position + (targetFlatFwd * distance) + (Vector3.up * height);
            transform.position = Vector3.SmoothDamp(transform.position, wantedPos, ref refVelocity, smoothSpeed);
            transform.LookAt(lookAtTarget);
        }
    }
}
