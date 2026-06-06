using UnityEngine;

namespace Helicopter_Game.Scripts
{
    public class IP_Base_RBController : MonoBehaviour
    {
        protected Rigidbody rb;
        
        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        protected virtual void FixedUpdate()
        {
            HandlePhysics();
        }

        protected virtual void HandlePhysics() { }
    }
}
