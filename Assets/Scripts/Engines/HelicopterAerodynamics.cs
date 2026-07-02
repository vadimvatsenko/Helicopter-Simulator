using UnityEngine;

public class HelicopterAerodynamics : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Drag Coefficients")]
    // Падение плашмя (ось Y) встречает самое большое сопротивление
    public float verticalDrag = 0.5f;   
    // Вперед (ось Z) вертолет обтекаемый
    public float forwardDrag = 0.1f;    
    // Боковое скольжение (ось X)
    public float sideDrag = 0.4f;       

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Важно: выключаем стандартное затухание Unity, 
        // чтобы оно не мешало нашим расчетам
        rb.linearDamping = 0f; 
    }

    void FixedUpdate()
    {
        // 1. Получаем локальную скорость вертолета
        Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity);

        // 2. Считаем силу сопротивления для каждой оси индивидуально (пропорционально квадрату скорости)
        // Знак (Mathf.Sign) нужен, чтобы сила всегда была направлена ПРОТИВ движения
        float dragX = -Mathf.Sign(localVelocity.x) * (localVelocity.x * localVelocity.x) * sideDrag;
        float dragY = -Mathf.Sign(localVelocity.y) * (localVelocity.y * localVelocity.y) * verticalDrag;
        float dragZ = -Mathf.Sign(localVelocity.z) * (localVelocity.z * localVelocity.z) * forwardDrag;

        // 3. Собираем локальный вектор сил сопротивления
        Vector3 localDragForce = new Vector3(dragX, dragY, dragZ);

        // 4. Переводим силу в мировые координаты и прикладываем к Rigidbody
        Vector3 worldDragForce = transform.TransformDirection(localDragForce);
        
        // Масса учитывается автоматически, если используем ForceMode.Force
        rb.AddForce(worldDragForce, ForceMode.Force);
    }
}