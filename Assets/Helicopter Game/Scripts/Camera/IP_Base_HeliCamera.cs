using System;
using UnityEngine;

namespace Helicopter_Game.Scripts.Camera
{
    public class IP_Base_HeliCamera : MonoBehaviour
    {
        [Header("Base Camera Properties")]
        [SerializeField] protected Rigidbody rb;
        [SerializeField] protected Transform lookAtTarget;
        
        protected Vector3 WantedPos;
        protected Vector3 RefVelocity;
        protected Vector3 TargetFlatFwd;
        protected Action UpdateEvent;

        public Rigidbody Rb => rb;
        
        protected virtual void FixedUpdate()
        {
            
            TargetFlatFwd = rb.transform.forward;
            TargetFlatFwd.y  = 0;
            TargetFlatFwd.Normalize();
            
            UpdateEvent?.Invoke();
        }

        protected void HandleCamera() { }
    }
}
