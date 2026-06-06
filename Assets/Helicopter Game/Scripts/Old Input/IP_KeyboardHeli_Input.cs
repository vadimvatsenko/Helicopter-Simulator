using Helicopter_Game.Scripts.Old_Input;
using UnityEngine;

public class IP_KeyboardHeli_Input : IP_BaseHeli_Input
{
    private const string PEDAL_INPUT = "Pedal";
    private const string COLLECTIVE_INPUT = "Collective";
    private const string CYCLE_INPUT = "Cyclic";
    private const string THROTTLE_INPUT = "Throttle";
    
    
    [Header("Heli KeyBoard Inputs")]
    public float ThrottleInput { get; private set; } = 0f;
    public float CollectiveInput { get; private set; } = 0f;
    public Vector2 CyclicInput { get; private set; } = Vector2.zero;
    public float PedalInput { get; private set; } = 0f;

    protected override void HandleInput()
    {
        base.HandleInput();
        
        HandleThrottle();
        HandlePedal();
        HandleCollective();
        HandleCyclic();
    }

    private void HandleThrottle()
    {
        ThrottleInput = Input.GetAxis(THROTTLE_INPUT);
    }
    
    private void HandlePedal()
    {
        PedalInput = Input.GetAxis(PEDAL_INPUT);
    }
    
    private void HandleCollective()
    {
        CollectiveInput = Input.GetAxis(COLLECTIVE_INPUT);
    }

    private void HandleCyclic()
    {
        float x = horizontalInput;
        float y = verticalInput;
        CyclicInput = new Vector2(x, y);
    }
} 
