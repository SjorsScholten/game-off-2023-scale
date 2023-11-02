using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // movement data
    public Movement move;
    public Jumping jump;
    public Gravity gravity;

    // Player Input
    public Vector2 moveInput;

    // Player State
    public Idle idleState;
    public Moving movingState;
    public Falling fallingState;
    public StateMachine<Player> stateMachine;

    public CharacterBody characterBody;

    private void Awake()
    {
        if(!characterBody)
            characterBody = GetComponent<CharacterBody>();

        InitializePlayerState();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        stateMachine.HandleInput();
        stateMachine.HandleUpdate();
    }

    private void InitializePlayerState()
    {
        idleState = new Idle(this);
        movingState = new Moving(this);
        fallingState = new Falling(this);

        stateMachine = new StateMachine<Player>(idleState);
    }
}
