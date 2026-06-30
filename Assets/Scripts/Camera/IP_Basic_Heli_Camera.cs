using UnityEngine;

namespace Camera
{
    public class IP_Basic_Heli_Camera : IP_Base_HeliCamera
    {
        [Header("Basic Camera Properties")] 
        [SerializeField] private float height = 5f;
        [SerializeField] private float distance = 5f;
        [SerializeField] private float smoothSpeed = 0.35f;

        private void OnEnable() => UpdateEvent += UpdateCamera;
        private void OnDisable() => UpdateEvent -= UpdateCamera;
        
        protected virtual void UpdateCamera()
        {
            WantedPos = rb.position + (TargetFlatFwd * distance) + (Vector3.up * height);
            transform.position = Vector3.SmoothDamp(transform.position, WantedPos, ref RefVelocity, smoothSpeed);
            transform.LookAt(lookAtTarget);
        }
    }
}
