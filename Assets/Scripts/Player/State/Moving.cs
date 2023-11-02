using UnityEngine;

public class Moving : Grounded
{
    public Moving(Player source) : base(source)
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
        if(Mathf.Abs(source.moveInput.x) < 0.01f) {
            return source.idleState;
        }
        return base.HandleInput();
    }

    public override void HandleUpdate()
    {
        var motion = source.moveInput * source.move.speed;
        source.characterBody.MoveAndSlide(motion);
    }
}