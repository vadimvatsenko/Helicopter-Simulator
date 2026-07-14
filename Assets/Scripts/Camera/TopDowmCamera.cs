using UnityEngine;

namespace Camera
{
    public class TopDowmCamera : BaseHeliCamera, IHeliCamera
    {
        [Header("TopDown Camera Properties")] 
        [SerializeField] private float height = 2f;
        [SerializeField] private float distance = 2f;
        [SerializeField] private float leadDistance = 0.25f;
        [SerializeField] private float smoothTime = 0.15f;

        private Vector3 _finalPos;
        private Vector3 _finalLead;
        private Vector3 _refLeadVelocity;
        private void OnEnable() => UpdateEvent += UpdateCamera;
        private void OnDisable() => UpdateEvent -= UpdateCamera;
        
        public void UpdateCamera()
        {
            WantedPos = (Vector3.back * -distance) + (Vector3.up * height);
            
            Vector3 targetPos = Rb.position;
            targetPos.y = 0f;
            
            Vector3 lead = Rb.linearVelocity;
            lead.y = 0f;
            
            _finalPos = Vector3.SmoothDamp(_finalPos, targetPos + WantedPos, ref RefVelocity, smoothTime);
            _finalLead = Vector3.SmoothDamp(_finalLead, (lead * leadDistance), ref _refLeadVelocity, smoothTime);
            transform.position = _finalPos;
            transform.LookAt(lookAtTarget.position + _finalLead);
        }
    }
}
