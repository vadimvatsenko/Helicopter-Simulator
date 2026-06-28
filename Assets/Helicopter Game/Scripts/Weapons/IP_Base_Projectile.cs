using System;
using UnityEngine;

namespace Helicopter_Game.Scripts.Weapons
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class IP_Base_Projectile : MonoBehaviour
    {
        [Header("Base Projectile Properties")]
        [SerializeField] private float projectileSpeed = 200f;
        [SerializeField] private float damagePower = 50f;
        [SerializeField] private float lifetime = 5f;
        
        protected Rigidbody Rb;
        protected SphereCollider col;

        private void Start()
        {
            Rb = GetComponent<Rigidbody>();
            col = GetComponent<SphereCollider>();
            
            col.isTrigger = true;

            FireProjectile();
        }

        public virtual void FireProjectile()
        {
            Rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
            Destroy(gameObject, lifetime);
        }
    }
}