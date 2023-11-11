using System;
using UnityEngine;

[Serializable]
public class Jump
{
    public float height;

    public Vector2 CalculateJumpVelocity(Vector2 velocity, Gravity gravity, Vector2 normal)
    {
        var speed = Mathf.Sqrt(2 * gravity.strength * height);
        var direction = normal;

        return direction * speed; 
    }
}
