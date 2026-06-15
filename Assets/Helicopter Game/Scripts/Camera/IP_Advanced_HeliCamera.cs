using UnityEngine;

namespace Helicopter_Game.Scripts.Camera
{
    public class IP_Advanced_HeliCamera : IP_Base_HeliCamera
    {
        [Header("Advanced Camera Properties")] 
        [SerializeField] private float height = 5;
        [SerializeField] private float minDistance = 4f;
        [SerializeField] private float maxDistance = 8f;
        [SerializeField] private float catchUpModifirier = 5f;
        private void OnEnable() => updateEvent += UpdateCamera;
        private void OnDisable() => updateEvent -= UpdateCamera;
        
        public void UpdateCamera()
        {
            // Get the flat direction
            Vector3 dirToTarget = transform.position - rb.position;
            dirToTarget.y = 0;
            Vector3 normilizedDir = dirToTarget.normalized;
            Debug.DrawRay(rb.position, dirToTarget, Color.green);
            
            // re-position camera
            wantedPos = rb.position + dirToTarget;
            float currentMagnitude = dirToTarget.magnitude;

            if (currentMagnitude < minDistance)
            {
                float delta =  minDistance - currentMagnitude;
                wantedPos += normilizedDir * (delta * Time.fixedDeltaTime * catchUpModifirier);
            }
            else if (currentMagnitude > maxDistance)
            {
                float delta =  currentMagnitude - maxDistance;
                wantedPos -= normilizedDir * (delta * Time.fixedDeltaTime * catchUpModifirier);
            }
            
            transform.position = wantedPos + (Vector3.up * height);
            transform.LookAt(lookAtTarget);
        }
    }
}
