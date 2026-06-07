using Helicopter_Game.Scripts.Rigidbodies;
using UnityEngine;

// Weight = Mass * Gravity

namespace Helicopter_Game.Scripts.Testing
{
    public class Forces : IP_Base_RBController
    {
        [SerializeField] private float maxSpeed;
        [SerializeField] private Vector3 movementDirection = Vector3.right;
    
        protected override void HandlePhysics() => rb.AddForce(movementDirection * maxSpeed, ForceMode.Impulse);
   
    }
}
