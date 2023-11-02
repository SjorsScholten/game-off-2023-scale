using System;
using UnityEngine;

public class CharacterBody : MonoBehaviour
{
    [Flags]
    public enum CollisionFlags {
        NONE        = 0,
        FLOOR       = 1 << 0,
        CEILING     = 1 << 1,
        WALL_RIGHT  = 1 << 2,
        WALL_LEFT   = 1 << 3
    }

    public ContactFilter2D contactFilter;
    public Vector2 position;
    public Vector2 velocity;
    public CollisionFlags _collision;
    
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(position);
    }

    private void MoveAndSlide() {
        MoveAndSlide(velocity);
    }

    public void MoveAndSlide(Vector2 motion) {
        var direction = motion.normalized;
        var distance = motion.magnitude * Time.deltaTime;

        var hits = new RaycastHit2D[1];
        var results = _collider2D.Cast(direction, contactFilter, hits, distance, true);
        if(results > 0) {
            
        }
        else {
            position += direction * distance;
        }
    }

    private void Trace(Vector2 direction, float distance) {
        var hits = new RaycastHit2D[1];
        var results = _collider2D.Cast(direction, contactFilter, hits, distance, true);
        if(results > 0) {
            //results[0].
        }
        else {
            return;
        }
    }

    public bool OnGround()
    {
        return _collision.HasFlag(CollisionFlags.FLOOR);
    }
}