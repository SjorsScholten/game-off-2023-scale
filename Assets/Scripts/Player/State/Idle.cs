using UnityEngine;

public class Idle : Grounded
{
    public Idle(Player source) : base(source)
    {
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override State<Player> HandleInput()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        if(Mathf.Abs(horizontalInput) > 0.01f) {
            return source.movingState;
        }

        return base.HandleInput();
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
    }
}