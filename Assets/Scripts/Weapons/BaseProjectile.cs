using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class BaseProjectile : MonoBehaviour
    {
        [Header("Base Projectile Properties")]
        [SerializeField] private float projectileSpeed = 200f;
        //[SerializeField] private float damagePower = 50f;
        [SerializeField] private float lifetime = 5f;
        
        protected Rigidbody Rb;
        protected SphereCollider Col;

        private void Start()
        {
            Rb = GetComponent<Rigidbody>();
            Col = GetComponent<SphereCollider>();
            
            Col.isTrigger = true;

            FireProjectile();
        }

        public virtual void FireProjectile()
        {
            Rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
            Destroy(gameObject, lifetime);
        }
    }
}