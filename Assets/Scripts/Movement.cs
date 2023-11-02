using System;
using UnityEngine;

[Serializable]
public class Movement
{
    public float speed;
    public float acceleration;

    public Vector2 CalculateMoveVelocity(Vector2 velocity, float direction, Vector2 normal)
    {
        return Vector2.zero;
    }
}
