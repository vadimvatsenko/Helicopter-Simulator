using Helicopter_Game.Scripts.Old_Input;
using UnityEngine;

public class IP_KeyboardHeli_Input : IP_BaseHeli_Input
{
    private const string PEDAL_INPUT = "Pedal";
    private const string COLLECTIVE_INPUT = "Collective";
    private const string CYCLE_INPUT = "Cyclic";
    private const string THROTTLE_INPUT = "Throttle";
    
    
    [Header("Heli KeyBoard Inputs")]
    public float ThrottleInput { get; protected set; } = 0f;
    public float CollectiveInput { get; protected set; } = 0f;
    public Vector2 CyclicInput { get; protected set; } = Vector2.zero;
    public float PedalInput { get; protected set; } = 0f;

    protected override void HandleInput()
    {
        base.HandleInput();
        
        HandleThrottle();
        HandlePedal();
        HandleCollective();
        HandleCyclic();

        ClampInputs();
    }

    protected virtual void HandleThrottle()
    {
        ThrottleInput = Input.GetAxis(THROTTLE_INPUT);
    }
    
    protected virtual void HandlePedal()
    {
        PedalInput = Input.GetAxis(PEDAL_INPUT);
    }
    
    protected virtual void HandleCollective()
    {
        CollectiveInput = Input.GetAxis(COLLECTIVE_INPUT);
    }

    protected virtual void HandleCyclic()
    {
        float x = horizontalInput;
        float y = verticalInput;
        CyclicInput = new Vector2(x, y);
    }
    
    protected void ClampInputs()
    {
        ThrottleInput = Mathf.Clamp(ThrottleInput, -1f, 1f);
        CollectiveInput = Mathf.Clamp(CollectiveInput, -1f, 1f);
        CyclicInput = new Vector2(Mathf.Clamp(CyclicInput.x, -1f, 1f), 
            Mathf.Clamp(CyclicInput.y, -1f, 1f));
        PedalInput = Mathf.Clamp(PedalInput, -1f, 1f);
    }
} 
