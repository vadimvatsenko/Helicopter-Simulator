using UnityEngine;
using UnityEngine.UI;

namespace Helicopter_Game.Scripts.Old_Input
{
    [RequireComponent(typeof(IP_KeyboardHeli_Input), typeof(IP_XboxHeli_Input), typeof(IP_MobileHeli_Input))]
    public class IP_Input_Controller : MonoBehaviour
    {
        [Header("Input Components")]
        [SerializeField] private InputType inputType = InputType.KeyBoard;
        private IP_KeyboardHeli_Input keyboardInput;
        private IP_XboxHeli_Input xboxInput;
        private IP_MobileHeli_Input mobileInput;
        
        /// <summary>
        /// 1. public float ThrottleInput { get; private set; }
        /// Перевод: Ввод дроссельной заслонки (газ).
        ///Что делает: Отвечает за управление мощностью двигателя (оборотами в минуту / RPM).
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

        private void Start()
        {
            keyboardInput = GetComponent<IP_KeyboardHeli_Input>();
            xboxInput = GetComponent<IP_XboxHeli_Input>();
            mobileInput = GetComponent<IP_MobileHeli_Input>();
            SetInputType(inputType);
        }

        private void Update()
        {
            /*Debug.Log("Throttle: " + ThrottleInput);
            Debug.Log("Collective: " + CollectiveInput);
            Debug.Log("Pedal: " + PedalInput);
            Debug.Log("StickyThrottle: " + StickyThrottle);
            Debug.Log("StickyCollective: " + StickyCollectiveInput);*/
            switch (inputType)
            {
                case InputType.KeyBoard:
                    ThrottleInput = keyboardInput.RawThrottleInput;
                    CollectiveInput = keyboardInput.CollectiveInput;
                    PedalInput = keyboardInput.PedalInput;
                    CyclicInput = keyboardInput.CyclicInput;
                    StickyThrottle = keyboardInput.StickyThrottle;
                    StickyCollectiveInput = keyboardInput.StickyCollectiveInput;
                    break;
                case InputType.XBox:
                    ThrottleInput = xboxInput.RawThrottleInput;
                    CollectiveInput = xboxInput.CollectiveInput;
                    PedalInput = xboxInput.PedalInput;
                    CyclicInput = xboxInput.CyclicInput;
                    StickyThrottle = xboxInput.StickyThrottle;  
                    StickyCollectiveInput = xboxInput.StickyCollectiveInput;
                    break;
                case InputType.Mobile:
                    //
                    break;
                default:
                    break;
            }
        }
        
        private void SetInputType(InputType type)
        {
            switch (type)
            {
                case InputType.KeyBoard:
                    inputType = InputType.KeyBoard;
                    keyboardInput.enabled = true;
                    xboxInput.enabled = false;
                    mobileInput.enabled = false;
                    break;
                case InputType.XBox:
                    inputType = InputType.XBox;
                    xboxInput.enabled = true;
                    keyboardInput.enabled = false;
                    mobileInput.enabled = false;
                    break;
                case InputType.Mobile:
                    inputType = InputType.Mobile;
                    mobileInput.enabled = true;
                    xboxInput.enabled = false;
                    keyboardInput.enabled = false;
                    break;
                default:
                    break;
            }
        }
    }
}
