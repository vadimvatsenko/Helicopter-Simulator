using Helicopter_Game.Scripts.Rigidbodies;
using UnityEngine;

namespace Helicopter_Game.Scripts.Testing
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
