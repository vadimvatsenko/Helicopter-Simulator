using UnityEngine;

namespace Camera
{
    public class CockPitHeliCamera : BaseHeliCamera, IHeliCamera
    {
        [Header("Cockpit Camera Properties")]
        [SerializeField] Transform cockpitPosition;
        [SerializeField] private Vector3 offset = Vector3.zero;
        //[SerializeField] private float fov = 70f;
        
        private Vector3 _startOffset;
        
        private void OnEnable()
        {
            UpdateEvent += UpdateCamera;
        }

        private void OnDisable()
        {
            UpdateEvent -= UpdateCamera;
        }

        public void UpdateCamera()
        {
            transform.position = cockpitPosition.position + _startOffset;
            transform.LookAt(lookAtTarget);
        }
    }
}
