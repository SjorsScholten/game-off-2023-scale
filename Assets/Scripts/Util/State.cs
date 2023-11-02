
using UnityEngine;

public abstract class State<T>
{
    public readonly T source;

    public State(T source)
    {
        this.source = source;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract State<T> HandleInput();
    public abstract void HandleUpdate();
}

public class StateMachine<T>
{
    public State<T> stateCurrent;

    public StateMachine(State<T> initialState)
    {
        initialState.Enter();
        stateCurrent = initialState;
    }

    public void HandleInput()
    {
        var stateNew = stateCurrent.HandleInput();
        if(stateNew != null) {
            stateCurrent.Exit();
            stateNew.Enter();
            stateCurrent = stateNew;
            Debug.Log($"entered state {stateCurrent.GetType().Name}");
        }
    }

    public void HandleUpdate()
    {
        stateCurrent.HandleUpdate();
    }
}