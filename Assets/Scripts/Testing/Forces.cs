using Rigidbodies;
using UnityEngine;

// Weight = Mass * Gravity

namespace Testing
{
    public class Forces : BaseRbController
    {
        [SerializeField] private float maxSpeed;
        [SerializeField] private Vector3 movementDirection = Vector3.right;
    
        protected override void HandlePhysics() => Rb.AddForce(movementDirection * maxSpeed, ForceMode.Impulse);
   
    }
}
