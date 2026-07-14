using Rigidbodies;
using UnityEngine;

namespace Testing
{
    public class Drag : BaseRbController
    {
        [Header("Drag Properties")]
        [SerializeField] private float dragFactor = 0.05f;
    
        protected override void HandlePhysics()
        {
            float currentSpeed = Rb.linearVelocity.magnitude;
            float finalDrag = dragFactor * currentSpeed;
            Rb.linearDamping = finalDrag;
        }
    }
}
