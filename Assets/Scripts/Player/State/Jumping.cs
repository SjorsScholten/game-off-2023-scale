using UnityEngine;

public class Jumping : OffWall
{
    public Jumping(Player source) : base(source, "Jumping")
    {
    }

    public override void Enter()
    {
        source.characterBody.velocity += source.jump.CalculateJumpVelocity(source.characterBody.velocity, source.gravity, Vector2.up);
        source.characterBody.MoveAndSlide();
    }

    public override void Exit()
    {
        
    }

    public override State<Player> HandleInput()
    {
        if(source.characterBody.velocity.y < 0)
            return source.fallingState;
        
        return base.HandleInput();
    }

    public override void HandleUpdate()
    {
        source.characterBody.velocity += source.gravity.GetAcceleration() * Time.deltaTime;
        source.characterBody.MoveAndSlide();
    }
}