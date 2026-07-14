using UnityEngine;
using UnityEngine.Events;

namespace Old_Input
{
    [RequireComponent(typeof(KeyboardHeliInput), typeof(XboxHeliInput), typeof(MobileHeliInput))]
    public class InputController : MonoBehaviour
    {
        [Header("Input Components")]
        [SerializeField] private InputType inputType = InputType.KeyBoard;
        [Space]
        [Header("Input Events")]
        [SerializeField] private UnityEvent onCameraButtonPressed = new UnityEvent();
        
        private KeyboardHeliInput _keyboardInput;
        private XboxHeliInput _xboxInput;
        private MobileHeliInput _mobileInput;
        
        /// <summary>
        /// 1. public float ThrottleInput { get; private set; }
        /// Перевод: Ввод дроссельной заслонки (газ).
        /// Что делает: Отвечает за управление мощностью двигателя (оборотами в минуту / RPM).
        /// В вертолетах шаг винта и обороты двигателя тесно связаны, но конкретно этот параметр регулирует,
        /// сколько "топлива/энергии" подается на двигатель.
        /// </summary>
        public float ThrottleInput { get; private set; }
        /// <summary>
        /// 2. public float CollectiveInput { get; private set; }
        /// Перевод: Ввод общего шага (коллективный шаг).
        /// Что делает: Управляет рычагом шаг-газа (Collective).
        /// Изменяет угол атаки (наклон) всех лопастей несущего винта одновременно.
        /// Именно это заставляет вертолет взлетать вертикально вверх или опускаться вниз.
        /// </summary>
        public float CollectiveInput { get; private set; }
        /// <summary>
        /// 3. public Vector2 CyclicInput { get; private set; }
        /// Перевод: Ввод циклического шага.
        /// Тип данных: Vector2 (содержит две координаты: X и Y), в отличие от остальных одномерных float.
        /// Что делает: Управляет ручкой циклического шага (Cyclic) — аналог джойстика или клавиш WASD.
        /// Координата X отвечает за крен (Roll) — наклон влево/вправо.
        /// Координата Y отвечает за тангаж (Pitch) — наклон носа вниз/вверх.
        ///Изменяет наклон лопастей в определенной точке их вращения, заставляя вертолет лететь вперед, назад или боком.
        /// </summary>
        public Vector2 CyclicInput  { get; private set; }
        /// <summary>
        /// 4. public float PedalInput { get; private set; }
        /// Перевод: Ввод с педалей (рыскание).
        /// Что делает: Управляет педалями, которые регулируют тягу хвостового винта (или перераспределяют мощность в соосных схемах).
        /// Отвечает за рыскание (Yaw) — поворот носа вертолета влево или вправо на месте.
        /// </summary>
        public float PedalInput { get; private set; }
        /// <summary>
        /// 5. public float StickyThrottle { get; private set; }
        /// Что делает: Сохраняет текущий уровень газа.
        /// Игрок нажал кнопку увеличения газа до 80%, отпустил кнопку — значение так и осталось 80% (оно "прилипло"),
        /// пока игрок намеренно не нажмет кнопку уменьшения газа.
        /// </summary>
        public float StickyThrottle { get; private set; }
        /// <summary>
        /// 6. public float StickyCollectiveInput { get; private set; }
        /// Что делает: То же самое, но для общего шага лопастей.
        /// Позволяет зафиксировать рычаг в определенном положении
        /// (например, для стабильного зависания в воздухе на одной высоте),
        /// чтобы игроку не нужно было непрерывно удерживать клавишу подъема/спуска.
        /// </summary>
        public float StickyCollectiveInput { get; private set; }
        public bool CameraInput { get; protected set; }
        
        public bool FireInput { get; protected set; }
        
        private void Start()
        {
            _keyboardInput = GetComponent<KeyboardHeliInput>();
            _xboxInput = GetComponent<XboxHeliInput>();
            _mobileInput = GetComponent<MobileHeliInput>();
            SetInputType(inputType);
        }

        private void Update()
        {
            switch (inputType)
            {
                case InputType.KeyBoard:
                    ThrottleInput = _keyboardInput.RawThrottleInput;
                    CollectiveInput = _keyboardInput.CollectiveInput;
                    PedalInput = _keyboardInput.PedalInput;
                    CyclicInput = _keyboardInput.CyclicInput;
                    StickyThrottle = _keyboardInput.StickyThrottle;
                    StickyCollectiveInput = _keyboardInput.StickyCollectiveInput;
                    CameraInput = _keyboardInput.CamInput;
                    FireInput = _keyboardInput.FireInput;
                    break;
                case InputType.XBox:
                    ThrottleInput = _xboxInput.RawThrottleInput;
                    CollectiveInput = _xboxInput.CollectiveInput;
                    PedalInput = _xboxInput.PedalInput;
                    CyclicInput = _xboxInput.CyclicInput;
                    StickyThrottle = _xboxInput.StickyThrottle;  
                    StickyCollectiveInput = _xboxInput.StickyCollectiveInput;
                    CameraInput = _xboxInput.CamInput;
                    FireInput = _xboxInput.FireInput;
                    break;
                case InputType.Mobile:
                    //
                    break;
                default:
                    break;
            }
            
            if (CameraInput)
            {
                onCameraButtonPressed?.Invoke();
            }
        }
        
        private void SetInputType(InputType type)
        {
            switch (type)
            {
                case InputType.KeyBoard:
                    inputType = InputType.KeyBoard;
                    _keyboardInput.enabled = true;
                    _xboxInput.enabled = false;
                    _mobileInput.enabled = false;
                    break;
                case InputType.XBox:
                    inputType = InputType.XBox;
                    _xboxInput.enabled = true;
                    _keyboardInput.enabled = false;
                    _mobileInput.enabled = false;
                    break;
                case InputType.Mobile:
                    inputType = InputType.Mobile;
                    _mobileInput.enabled = true;
                    _xboxInput.enabled = false;
                    _keyboardInput.enabled = false;
                    break;
                default:
                    break;
            }
        }
    }
}
