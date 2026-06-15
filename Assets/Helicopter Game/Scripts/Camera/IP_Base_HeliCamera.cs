using System;
using UnityEngine;

namespace Helicopter_Game.Scripts.Camera
{
    public class IP_Base_HeliCamera : MonoBehaviour
    {
        [Header("Base Camera Properties")]
        [SerializeField] protected Rigidbody rb;
        [SerializeField] protected Transform lookAtTarget;
        
        protected Vector3 wantedPos;
        protected Vector3 refVelocity;
        protected Action updateEvent;
        
        protected virtual void FixedUpdate()
        {
            updateEvent?.Invoke();
        }

        protected void HandleCamera() { }
    }
}
