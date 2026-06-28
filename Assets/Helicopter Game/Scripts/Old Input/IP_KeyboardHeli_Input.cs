using UnityEngine;

namespace Helicopter_Game.Scripts.Old_Input
{
    public class IP_KeyboardHeli_Input : IP_BaseHeli_Input
    {
        private const string PEDAL_INPUT = "Pedal";
        private const string COLLECTIVE_INPUT = "Collective";
        private const string CYCLE_INPUT = "Cyclic";
        private const string THROTTLE_INPUT = "Throttle";
    
        [Header("Heli KeyBoard Inputs")]
        public float RawThrottleInput { get; protected set; } = 0f;
        public float CollectiveInput { get; protected set; } = 0f;
        public Vector2 CyclicInput { get; protected set; } = Vector2.zero;
        public float PedalInput { get; protected set; } = 0f;
        /// <summary>
        /// 5. public float StickyThrottle { get; private set; }
        /// Что делает: Сохраняет текущий уровень газа.
        /// Игрок нажал кнопку увеличения газа до 80%, отпустил кнопку — значение так и осталось 80% (оно "прилипло"),
        /// пока игрок намеренно не нажмет кнопку уменьшения газа.
        /// </summary>
        public float StickyThrottle { get; protected set; } = 0f;
        public float StickyCollectiveInput { get; protected set; } = 0f;

        [Header("Camera Input Properties")] 
        [SerializeField] private KeyCode camButton = KeyCode.C;
        public bool CamInput { get; protected set; } = false;

        protected override void HandleInput()
        {
            base.HandleInput();
        
            // Input Methods
            HandleThrottle();
            HandlePedal();
            HandleCollective();
            HandleCyclic();
            HandleCamBtn();

            // Utils Methods
            ClampInputs();
            HandleStickyThrottle();
            HandleStickyCollective();
        }

        protected virtual void HandleThrottle() => RawThrottleInput = Input.GetAxis(THROTTLE_INPUT);
        
        protected virtual void HandlePedal() => PedalInput = Input.GetAxis(PEDAL_INPUT);
        
        protected virtual void HandleCollective() => CollectiveInput = Input.GetAxis(COLLECTIVE_INPUT);
        
        protected virtual void HandleCyclic()
        {
            float x = HorizontalInput;
            float y = VerticalInput;
            CyclicInput = new Vector2(x, y);
        }
    
        protected void ClampInputs()
        {
            RawThrottleInput = Mathf.Clamp(RawThrottleInput, -1f, 1f);
            CollectiveInput = Mathf.Clamp(CollectiveInput, -1f, 1f);
            CyclicInput = new Vector2(Mathf.Clamp(CyclicInput.x, -1f, 1f), 
                Mathf.Clamp(CyclicInput.y, -1f, 1f));
            PedalInput = Mathf.Clamp(PedalInput, -1f, 1f);
        }

        protected void HandleStickyThrottle()
        {
            StickyThrottle += RawThrottleInput * Time.deltaTime;
            StickyThrottle = Mathf.Clamp01(StickyThrottle);
        }

        protected void HandleStickyCollective()
        {
            StickyCollectiveInput += -CollectiveInput * Time.deltaTime;
            StickyCollectiveInput = Mathf.Clamp01(StickyCollectiveInput);
        }
        
        protected virtual void HandleCamBtn()
        {
            CamInput = Input.GetKeyDown(camButton);
        }
    }
} 
