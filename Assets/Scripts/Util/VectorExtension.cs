using UnityEngine;

public static class VectorExtension
{
    public static Vector2 Project(Vector2 a, Vector2 b)
    {
        return a - b * Vector2.Dot(a, b);
    }
}