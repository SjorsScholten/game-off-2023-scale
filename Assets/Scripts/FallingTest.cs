using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTest : MonoBehaviour
{
    public Gravity gravity;
    private Vector2 _velocity;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _velocity += gravity.GetAcceleration() * Time.deltaTime;
        _transform.position += (Vector3)(_velocity * Time.deltaTime);
    }
}
