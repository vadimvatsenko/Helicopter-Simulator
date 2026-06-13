using UnityEngine;

namespace Helicopter_Game.Scripts.Rigidbodies
{
    [RequireComponent(typeof(Rigidbody))]
    public class IP_Base_RBController : MonoBehaviour
    {
        private const float PndToKg = 0.454f;
        private const float KgToPnd = 2.20462f;
        
        [Header("Weight Properties In Pounds")] 
        [SerializeField] private float weightInPnd = 100f;
        // объект центра тяжести
        protected Transform cog;
        
        protected Rigidbody rb;
        protected float weight;

        public Transform Cog
        {
            get => cog;
            set => cog = value;
        }
        
        protected virtual void Start()
        {
            float finalKg = weightInPnd * PndToKg;
            weight = finalKg;
            rb = GetComponent<Rigidbody>();
            if (rb) rb.mass = weight;
        }

        protected virtual void FixedUpdate()
        {
            HandlePhysics();
        }

        protected virtual void HandlePhysics() { }
    }
}
