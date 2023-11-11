using UnityEngine;

public class Grounded : OffWall
{
    public Grounded(Player source, string name) : base(source, "Grounded/" + name)
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
        if(source.jumpRequest.IsValid()) {
            return source.jumpingState;
        }

        if(!source.characterBody.OnFloor()) {
            return source.fallingState;
        }

        return base.HandleInput();
    }

    public override void HandleUpdate()
    {
        source.characterBody.velocity = Vector2.MoveTowards(source.characterBody.velocity, Vector2.zero, source.move.acceleration * Time.deltaTime);
        source.characterBody.MoveAndSlide();
    }
}