using UnityEngine;

public class Torque : MonoBehaviour
{
    #region Variables

    [SerializeField] private float torqueSpeed = 2f;
    private Rigidbody rb;
    #endregion
    
    #region Builting Methods
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
    #endregion
}
