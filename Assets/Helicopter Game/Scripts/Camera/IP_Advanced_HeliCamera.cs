using UnityEngine;
using UnityEngine.Serialization;

namespace Helicopter_Game.Scripts.Camera
{
    public class IP_Advanced_HeliCamera : IP_Base_HeliCamera
    {
        [Header("Advanced Camera Properties")] 
        [SerializeField] private float height = 5;
        [SerializeField] private float minDistance = 4f;
        [SerializeField] private float maxDistance = 8f;
        [SerializeField] private float catchUpModifirier = 5f;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float minVelocityForOrient = 5f;
        [SerializeField] private bool unUseHeliSelfRotate = true;
        
        private float finalAngle;
        private Vector3 wantedDir;
        
        private void OnEnable() => updateEvent += UpdateCamera;
        private void OnDisable() => updateEvent -= UpdateCamera;
        
        public void UpdateCamera()
        {
            // Get the flat direction
            Vector3 dirToTarget = transform.position - rb.position;
            dirToTarget.y = 0;
            Vector3 normilizedDir = dirToTarget.normalized;
            wantedDir = normilizedDir;
            Debug.DrawRay(rb.position, wantedDir, Color.green);
            
            // Find the angle between our Dir Vector and our Flat Forward
            float angleToFwd = Vector3.SignedAngle(normilizedDir, targetFlatFwd, Vector3.up);

            float wantedAngle = 0f;
            if (unUseHeliSelfRotate)
            {
                if (rb.linearVelocity.magnitude > minVelocityForOrient)
                {
                    wantedAngle = angleToFwd * Time.fixedDeltaTime;
                }
            }
            else
            {
                wantedAngle = angleToFwd * Time.fixedDeltaTime;
            }
            
            finalAngle = Mathf.Lerp(finalAngle, wantedAngle, Time.fixedDeltaTime * rotationSpeed);
            wantedDir = Quaternion.AngleAxis(finalAngle, Vector3.up) * wantedDir;
            
            // re-position camera
            wantedPos = rb.position + (wantedDir * dirToTarget.magnitude);
            float currentMagnitude = dirToTarget.magnitude;

            if (currentMagnitude < minDistance)
            {
                float delta =  minDistance - currentMagnitude;
                wantedPos += wantedDir * (delta * Time.fixedDeltaTime * catchUpModifirier);
            }
            else if (currentMagnitude > maxDistance)
            {
                float delta =  currentMagnitude - maxDistance;
                wantedPos -= wantedDir * (delta * Time.fixedDeltaTime * catchUpModifirier);
            }
            
            //Apply final Transformation
            transform.position = wantedPos + (Vector3.up * height);
            transform.LookAt(lookAtTarget);
        }
    }
}
