using UnityEngine;

namespace Helicopter_Game.Scripts
{
    public class Drag : IP_Base_RBController
    {
        [Header("Drag Properties")]
        [SerializeField] private float dragFactor = 0.05f;
    
        protected override void HandlePhysics()
        {
            float currentSpeed = rb.linearVelocity.magnitude;
            float finalDrag = dragFactor * currentSpeed;
            rb.linearDamping = finalDrag;
        }
    }
}
