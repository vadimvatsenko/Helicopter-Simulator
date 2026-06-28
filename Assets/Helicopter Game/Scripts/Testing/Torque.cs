using UnityEngine;

namespace Helicopter_Game.Scripts.Testing
{
    public class Torque : MonoBehaviour
    {
        
        [SerializeField] private float torqueSpeed = 2f;
        private Rigidbody _rb;
        
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_rb)
            {
                _rb.AddTorque(Vector3.up * torqueSpeed);
            }
        }
    }
}
