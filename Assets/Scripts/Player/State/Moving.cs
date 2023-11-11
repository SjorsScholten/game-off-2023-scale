using UnityEngine;

public class Moving : Grounded
{
    public Moving(Player source) : base(source, "Moving")
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
        var desiredSpeed    = source.moveInput.x * source.move.speed;
        var maxSpeedChange  = source.move.acceleration * Time.deltaTime;
        var moveAxis        = VectorExtension.Project(Vector2.right, source.characterBody.groundNormal).normalized;
        var alignedSpeed    = Vector2.Dot(source.characterBody.velocity, moveAxis);
        var finalSpeed      = Mathf.MoveTowards(alignedSpeed, desiredSpeed, maxSpeedChange);
        
        source.characterBody.velocity += moveAxis * (finalSpeed - alignedSpeed);
        source.characterBody.MoveAndSlide();
    }
}