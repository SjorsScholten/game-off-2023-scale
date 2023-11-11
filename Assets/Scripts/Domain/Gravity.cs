using System;
using UnityEngine;

[Serializable]
public class Gravity
{
    public float strength;
    public Vector2 direction;

    public Vector2 GetAcceleration()
    {
        return direction * strength;
    }
}
