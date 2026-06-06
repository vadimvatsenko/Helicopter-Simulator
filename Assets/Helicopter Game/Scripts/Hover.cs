using UnityEngine;

// Пример
// Получаем чистую силу гравитации (например, 9.81)
// float gravityForce = Physics.gravity.magnitude;
// Используем её для расчета скорости падения
// verticalVelocity -= gravityForce * Time.deltaTime;

namespace Helicopter_Game.Scripts
{
    public class Hover : IP_Base_RBController
    {
        [Header("Hover Properties")] 
        [SerializeField] private float hoverHight = 3f;
        [SerializeField] private Transform hoverPosition;
        [SerializeField] private float groundCheckDistance = Mathf.Infinity;
        [SerializeField] private LayerMask whatIsGround;
        private bool isGroundDetection;
        private float currentDistanceToGround;
        
        [Header("Torque Properties")]
        [SerializeField] private float torqueForce = 4f;
        
        [Header("Drag Properties")]
        [SerializeField] private float dragFactor = 5f;
        
        private float weight;
        
        protected override void Start()
        {
            base.Start();
            weight = rb.mass * Physics.gravity.magnitude;
        }

    
        protected override void FixedUpdate()
        {
            HandlePhysics();
            CalculateGroundDistance();
            HandleTorque();
            HandleDrag();
        }

        private void HandleDrag() => rb.linearDamping = dragFactor * rb.linearVelocity.magnitude;

        private void HandleTorque() => rb.AddTorque(Vector3.up * torqueForce);
        
        private void CalculateGroundDistance()
        {
            RaycastHit hit;
            isGroundDetection = Physics.Raycast(hoverPosition.position, Vector3.down, out hit, groundCheckDistance, whatIsGround);

            if (isGroundDetection)
            {
                currentDistanceToGround = hit.distance;
            }
        }

        protected override void HandlePhysics()
        {
            float groundDiff = hoverHight - currentDistanceToGround;
            Vector3 finalHoverForce = Vector3.up * (groundDiff + 1);
            rb.AddForce(finalHoverForce * weight);
        }

        private void OnDrawGizmos()
        {
            float gizmoDistance = float.IsInfinity(groundCheckDistance) ? 100f : groundCheckDistance;
            Gizmos.color = isGroundDetection ? Color.green : Color.red;
            Gizmos.DrawRay(hoverPosition.position, Vector3.down * gizmoDistance);
        }
    }
}
