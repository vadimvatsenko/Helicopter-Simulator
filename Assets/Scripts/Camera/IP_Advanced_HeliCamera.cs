using UnityEngine;

namespace Camera
{
    public class IP_Advanced_HeliCamera : IP_Base_HeliCamera
    {
        [Header("Advanced Camera Properties")] 
        [SerializeField] private float height = 5;
        [SerializeField] private float minGroundHeight = 4f;
        [SerializeField] private float minDistance = 4f;
        [SerializeField] private float maxDistance = 8f;
        [SerializeField] private float catchUpModifirier = 5f;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float minVelocityForOrient = 5f;
        [SerializeField] private bool unUseHeliSelfRotate = true;
        
        private float _finalAngle;
        private Vector3 _wantedDir;
        private float _finalHeight;

        public float MinDistance
        {
            get => minDistance;
            set => minDistance = value;
        }

        public float MaxDistance
        {
            get => maxDistance;
            set => maxDistance = value;
        }
        
        private void OnEnable() => UpdateEvent += UpdateCamera;
        private void OnDisable() => UpdateEvent -= UpdateCamera;
        
        public void UpdateCamera()
        {
            // Get the flat direction
            Vector3 dirToTarget = transform.position - rb.position;
            dirToTarget.y = 0;
            Vector3 normalizedDir = dirToTarget.normalized;
            _wantedDir = normalizedDir;
            Debug.DrawRay(rb.position, _wantedDir, Color.green);
            
            // Find the angle between our Dir Vector and our Flat Forward
            float angleToFwd = Vector3.SignedAngle(normalizedDir, TargetFlatFwd, Vector3.up);

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
            
            _finalAngle = Mathf.Lerp(_finalAngle, wantedAngle, Time.fixedDeltaTime * rotationSpeed);
            _wantedDir = Quaternion.AngleAxis(_finalAngle, Vector3.up) * _wantedDir;
            
            // re-position camera
            WantedPos = rb.position + (_wantedDir * dirToTarget.magnitude);
            float currentMagnitude = dirToTarget.magnitude;

            if (currentMagnitude < minDistance)
            {
                float delta =  minDistance - currentMagnitude;
                WantedPos += _wantedDir * (delta * Time.fixedDeltaTime * catchUpModifirier);
            }
            else if (currentMagnitude > maxDistance)
            {
                float delta =  currentMagnitude - maxDistance;
                WantedPos -= _wantedDir * (delta * Time.fixedDeltaTime * catchUpModifirier);
            }
            
            // Take into account the height from the ground
            float wantedHeight = height;
            RaycastHit hit;
            Ray groundRay = new Ray(transform.position, Vector3.down);
            if (Physics.Raycast(groundRay, out hit, 100f))
            {
                if (hit.collider.gameObject.CompareTag("Ground") && hit.distance < minGroundHeight)
                {
                    wantedHeight = minGroundHeight - hit.distance;
                }
            }
            _finalHeight = Mathf.Lerp(_finalHeight, wantedHeight, Time.fixedDeltaTime * 2f);
            
            //Apply final Transformation
            transform.position = WantedPos + (Vector3.up * _finalHeight);
            transform.LookAt(lookAtTarget);
        }
    }
}
