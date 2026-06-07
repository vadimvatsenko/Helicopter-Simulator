using UnityEngine;

namespace Helicopter_Game.Scripts.Testing
{
    public class Torque : MonoBehaviour
    {
        
        [SerializeField] private float torqueSpeed = 2f;
        private Rigidbody rb;
        
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (rb)
            {
                rb.AddTorque(Vector3.up * torqueSpeed);
            }
        }
    }
}
