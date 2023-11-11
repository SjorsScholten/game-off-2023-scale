using UnityEngine;

public class OffWall : State<Player>
{
    public OffWall(Player source, string name) : base(source, "OffWall/" + name)
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
        if(Input.GetKeyDown(KeyCode.X))
        {
            return source.climbingState;
        }

        return null;
    }

    public override void HandleUpdate()
    {
        
    }
}