using UnityEngine;

public class Climbing : OnWall
{
    public Climbing(Player source) : base(source, "Climbing")
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
        return base.HandleInput();
    }

    public override void HandleUpdate()
    {
        var desiredSpeed    = source.move.speed * source.moveInput.magnitude;
        var maxSpeedChange  = source.move.acceleration * Time.deltaTime;
        var direction       = source.moveInput.normalized;
        var alignedSpeed    = Vector2.Dot(source.characterBody.velocity, direction);
        var finalSpeed      = Mathf.MoveTowards(alignedSpeed, desiredSpeed, maxSpeedChange);
        
        source.characterBody.velocity += direction * (finalSpeed - alignedSpeed);
        source.characterBody.MoveAndSlide();
    }
}