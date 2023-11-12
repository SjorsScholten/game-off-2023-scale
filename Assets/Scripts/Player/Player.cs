using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // movement data
    public Move move;
    public Jump jump;
    public Gravity gravity;

    // Player Input
    public Vector2 moveInput;
    public InputRequest jumpRequest;


    // Player State
    public Idle idleState;
    public Moving movingState;
    public Falling fallingState;
    public Jumping jumpingState;
    public Climbing climbingState;
    public StateMachine<Player> stateMachine;

    public CharacterBody characterBody;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(20, 20, 200, 200));
        GUILayout.Label($"State: {stateMachine.stateCurrent.name}");
        GUILayout.Label($"Collision: {characterBody.collision}");
        GUILayout.Label($"Velocity: {characterBody.velocity}");
        GUILayout.Label($"groundNormal: {characterBody.groundNormal}");
        GUILayout.EndArea();
    }

    private void Awake()
    {
        if(!characterBody)
            characterBody   = GetComponent<CharacterBody>();
        spriteRenderer      = GetComponent<SpriteRenderer>();
        animator            = GetComponent<Animator>();

        InitializePlayerState();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Z))
            jumpRequest.Request();

        stateMachine.HandleInput();
        stateMachine.HandleUpdate();
    }

    private void InitializePlayerState()
    {
        idleState       = new Idle(this);
        movingState     = new Moving(this);
        fallingState    = new Falling(this);
        jumpingState    = new Jumping(this);
        climbingState   = new Climbing(this);
        stateMachine    = new StateMachine<Player>(idleState);
    }
}

[Serializable]
public class InputRequest
{
    public int bufferTimeMs;
    private int _requestBufferTimeMs = 0;

    public void Request()
    {
        _requestBufferTimeMs = (int)(Time.time * 1000) + bufferTimeMs;
    }

    public bool IsValid()
    {
        return (int)(Time.time * 1000) < _requestBufferTimeMs;
    }
}
