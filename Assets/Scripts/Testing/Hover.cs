using Rigidbodies;
using UnityEngine;

// Пример
// Получаем чистую силу гравитации (например, 9.81)
// float gravityForce = Physics.gravity.magnitude;
// Используем её для расчета скорости падения
// verticalVelocity -= gravityForce * Time.deltaTime;

namespace Testing
{
    public class Hover : IP_Base_RBController
    {
        [Header("Hover Properties")] 
        [SerializeField] private float hoverHight = 3f;
        [SerializeField] private Transform hoverPosition;
        [SerializeField] private float groundCheckDistance = Mathf.Infinity;
        [SerializeField] private LayerMask whatIsGround;
        private bool _isGroundDetection;
        private float _currentDistanceToGround;
        
        [Header("Torque Properties")]
        [SerializeField] private float torqueForce = 4f;
        
        [Header("Drag Properties")]
        [SerializeField] private float dragFactor = 5f;
        
        private float _weight;
        
        protected override void Start()
        {
            base.Start();
            _weight = Rb.mass * Physics.gravity.magnitude;
        }

    
        protected override void FixedUpdate()
        {
            HandlePhysics();
            CalculateGroundDistance();
            HandleTorque();
            HandleDrag();
        }

        private void HandleDrag() => Rb.linearDamping = dragFactor * Rb.linearVelocity.magnitude;

        private void HandleTorque() => Rb.AddTorque(Vector3.up * torqueForce);
        
        private void CalculateGroundDistance()
        {
            RaycastHit hit;
            _isGroundDetection = Physics.Raycast(hoverPosition.position, Vector3.down, out hit, groundCheckDistance, whatIsGround);

            if (_isGroundDetection)
            {
                _currentDistanceToGround = hit.distance;
            }
        }

        protected override void HandlePhysics()
        {
            float groundDiff = hoverHight - _currentDistanceToGround;
            Vector3 finalHoverForce = Vector3.up * (groundDiff + 1);
            Rb.AddForce(finalHoverForce * _weight);
        }

        private void OnDrawGizmos()
        {
            float gizmoDistance = float.IsInfinity(groundCheckDistance) ? 100f : groundCheckDistance;
            Gizmos.color = _isGroundDetection ? Color.green : Color.red;
            Gizmos.DrawRay(hoverPosition.position, Vector3.down * gizmoDistance);
        }
    }
}
