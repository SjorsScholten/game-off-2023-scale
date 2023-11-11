using UnityEngine;

public class OnWall : State<Player>
{
    public OnWall(Player source, string name) : base(source, "OnWall/" + name)
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
            return source.fallingState;
        }

        return null;
    }

    public override void HandleUpdate()
    {
        
    }
}