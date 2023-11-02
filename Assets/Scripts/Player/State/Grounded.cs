using UnityEngine;

public class Grounded : State<Player>
{
    public Grounded(Player source) : base(source)
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
        if(!source.characterBody.OnGround()) {
            return source.fallingState;
        }

        return null;
    }

    public override void HandleUpdate()
    {
        source.characterBody.velocity = Vector2.MoveTowards(source.characterBody.velocity, Vector2.zero, Time.deltaTime);
    }
}