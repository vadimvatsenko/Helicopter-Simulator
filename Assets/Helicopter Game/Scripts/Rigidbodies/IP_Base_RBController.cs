using UnityEngine;

namespace Helicopter_Game.Scripts.Rigidbodies
{
    [RequireComponent(typeof(Rigidbody))]
    public class IP_Base_RBController : MonoBehaviour
    {
        private const float PND_TO_KG = 0.454f;
        private const float KG_TO_PND = 2.20462f;
        
        [Header("Weight Properties In Pounds")] 
        [SerializeField] private float weightInPnd = 100f;
        // объект центра тяжести
        [SerializeField] private Transform cog;
        
        protected Rigidbody Rb;
        protected float Weight;

        public Transform Cog
        {
            get => cog;
            set => cog = value;
        }
        
        protected virtual void Start()
        {
            float finalKg = weightInPnd * PND_TO_KG;
            Weight = finalKg;
            Rb = GetComponent<Rigidbody>();
            if (Rb) Rb.mass = Weight;
        }

        protected virtual void FixedUpdate()
        {
            HandlePhysics();
        }

        protected virtual void HandlePhysics() { }
    }
}
