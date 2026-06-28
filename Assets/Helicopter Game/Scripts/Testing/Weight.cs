using UnityEngine;

namespace Helicopter_Game.Scripts.Testing
{
    public class Weight : MonoBehaviour
    {
        private const float PND_TO_KG = 0.454f;
        private const float KG_TO_PND = 2.20462f;
    
        private float _weight;

        [Header("Weight Properties In Pounds")] 
        [SerializeField] private float weightInPnd = 10f;
        private Rigidbody _rb;
    
        private void Start()
        {
            float finalKg = weightInPnd * PND_TO_KG;
            _weight = finalKg;
            _rb = GetComponent<Rigidbody>();
        
            if (_rb) _rb.mass = finalKg;
        }
    }
}
