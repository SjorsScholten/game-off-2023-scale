using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector2 offset;
    public float smoothTime;
    private Vector2 smoothVelocity;
    public Bounds boundary;
    public Vector2 position;

    private Transform _transform;
    private Camera _camera;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boundary.center, boundary.size);
    }

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _camera = GetComponent<Camera>();

        position = _transform.position;
    }

    private void Update()
    {
        // smoothly move camera towards target
        position = Vector2.SmoothDamp(position, (Vector2)target.position + offset, ref smoothVelocity, smoothTime);

        // Clamp position between boundary
        var xOffset = _camera.orthographicSize * _camera.aspect;
        var xMin    = boundary.min.x + xOffset;
        var xMax    = boundary.max.x - xOffset;
        var yMin    = boundary.min.y + _camera.orthographicSize;
        var yMax    = boundary.max.y - _camera.orthographicSize;
        position.x  = Mathf.Clamp(position.x, xMin, xMax);
        position.y  = Mathf.Clamp(position.y, yMin, yMax);

        // Move camera to new position without changing the z
        _transform.Translate(position - (Vector2)_transform.position);
    }
}