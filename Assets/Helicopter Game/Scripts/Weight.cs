using UnityEngine;

public class Weight : MonoBehaviour
{
    private const float PndToKg = 0.454f;
    private const float KgToPnd = 2.20462f;
    
    private float weight;

    [Header("Weight Properties In Pounds")] 
    [SerializeField] private float weightInPnd = 10f;
    private Rigidbody rb;
    
    private void Start()
    {
        float finalKg = weightInPnd * PndToKg;
        weight = finalKg;
        rb = GetComponent<Rigidbody>();
        
        if (rb) rb.mass = finalKg;
    }
}
