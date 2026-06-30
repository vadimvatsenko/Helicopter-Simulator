using Rigidbodies;
using UnityEngine;

namespace Testing
{
    public class Drag : IP_Base_RBController
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
