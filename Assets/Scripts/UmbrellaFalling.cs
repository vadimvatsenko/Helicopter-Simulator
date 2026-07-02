using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UmbrellaFalling : MonoBehaviour
{
    // Ссылки на визуальные объекты (модели) внутри префаба
    [Header("Visual States")]
    [SerializeField] private GameObject closedVisual; // Ссылка на модель закрытого зонтика
    [SerializeField] private GameObject openVisual;   // Ссылка на модель открытого зонтика

    // Настройки физики для разных состояний
    [Header("Physics Settings")]
    [SerializeField] private float closedDrag = 0.05f; // Стандартное сопротивление воздуха
    [SerializeField] private float openDrag = 2.0f;     // Высокое сопротивление (эффект парашюта)
    [SerializeField] private float closedAngularDrag = 0.05f;
    [SerializeField] private float openAngularDrag = 1.0f; // Чтобы открытый меньше вращался
    [SerializeField] private bool isOpen = false;

    private Rigidbody _rb;
    

    // Свойство для получения состояния из других скриптов
    public bool IsOpen => isOpen;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        UpdateStateVisuals(); // Устанавливаем начальный вид
    }

    // Метод для переключения состояния (можно вызывать кнопкой или триггером)
    public void ToggleOpen()
    {
        isOpen = !isOpen;
        UpdateStateVisuals();
    }

    // Принудительно установить состояние (например, при спавне)
    public void SetState(bool open)
    {
        isOpen = open;
        UpdateStateVisuals();
    }

    // Самая главная инженерная часть: обновление физики и визуала
    private void UpdateStateVisuals()
    {
        // 1. Включаем нужную модель, выключаем ненужную
        if (closedVisual != null) closedVisual.SetActive(!isOpen);
        if (openVisual != null) openVisual.SetActive(isOpen);

        // 2. Меняем физику Rigidbody
        if (isOpen)
        {
            // Открытый зонтик: падает медленно ("парашютирует")
            _rb.linearDamping = openDrag;
            _rb.angularDamping = openAngularDrag;
        }
        else
        {
            // Закрытый зонтик: падает быстро
            _rb.linearDamping = closedDrag;
            _rb.angularDamping = closedAngularDrag;
        }
    }

    // Необязательно: логика для VR-взаимодействия
    // Если используешь XR Interaction Toolkit, можно переключать состояние при взятии
    /*
    public void OnSelectEntered(UnityEngine.XR.Interaction.Toolkit.SelectEnterEventArgs args)
    {
        // Например, закрывать при взятии в руку
        SetState(false);
    }
    */
}