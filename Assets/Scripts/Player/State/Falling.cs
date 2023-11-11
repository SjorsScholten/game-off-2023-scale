using UnityEngine;

public class Falling : OffWall
{
    public Falling(Player source) : base(source, "Falling")
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
        if(source.characterBody.OnFloor()) {
            if(Mathf.Abs(source.moveInput.x) > 0.01f)
                return source.movingState;
            else
                return source.idleState;
        }

        return null;
    }

    public override void HandleUpdate()
    {
        source.characterBody.velocity += source.gravity.GetAcceleration() * Time.deltaTime;
        source.characterBody.MoveAndSlide();
    }
}