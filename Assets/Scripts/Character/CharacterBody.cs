using System;
using UnityEngine;

public class CharacterBody : MonoBehaviour
{
    const int MAX_SLIDES = 5;
    [Flags]
    public enum CollisionFlags {
        NONE        = 0,
        FLOOR       = 1 << 0,
        CEILING     = 1 << 1,
        WALL_UP     = CEILING,
        WALL_DOWN   = FLOOR,
        WALL_RIGHT  = 1 << 2,
        WALL_LEFT   = 1 << 3,
    }

    public readonly struct SnapShot
    {
        public readonly Vector2 position;
        public readonly Vector2 velocity;
        public readonly CollisionFlags collision;

        public SnapShot(Vector2 position, Vector2 velocity, CollisionFlags collision)
        {
            this.position = position;
            this.velocity = velocity;
            this.collision = collision;
        }
    }

    public struct Motion
    {
        public Vector2 direction;
        public float distance;

        public Motion(Vector2 direction, float distance)
        {
            this.direction = direction;
            this.distance = distance;
        }
    }

    public readonly struct TraceResult
    {
        public readonly bool collided;
        public readonly Vector2 normal;
        public readonly float penetration;
        public readonly float travel;
        public readonly float remainder;
        public readonly Vector2 projection;

        public TraceResult(bool collided, Vector2 normal, float penetration, float travel, float remainder, Vector2 projection)
        {
            this.collided = collided;
            this.normal = normal;
            this.penetration = penetration;
            this.travel = travel;
            this.remainder = remainder;
            this.projection = projection;
        }

        public Motion GetResponseMotion()
        {
            return new Motion(projection, remainder);
        }
    }

    public float skinWidth;
    public ContactFilter2D contactFilter;

    public Vector2 position;
    public Vector2 velocity;
    public CollisionFlags collision;
    public Vector2 groundNormal;

    private Vector2 _velocityInternal;
    private Bounds _bounds;
    private SnapShot _previousState;
    
    private Transform _transform;
    private Collider2D _collider2D;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(position, _bounds.size);
    }

    private void OnValidate()
    {
        Awake();
    }
    
    private void Awake()
    {
        GetComponentsOnObject();
        InitializeValues();
    }

    private void Update()
    {
        if(Overlaps(position))
        {
            //RestoreState(_previousState);
        }

        _previousState = CaptureState();

        collision = CollisionFlags.NONE;
        var distance = 2 * skinWidth;
        
        var result = Trace(new Motion(Vector2.down, distance));
        if(result.collided)
        {
            groundNormal = result.normal;
            collision |= CollisionFlags.FLOOR;
        }
        else
        {
            groundNormal = Vector2.up;
        }

        if(Trace(new Motion(Vector2.up, distance)).collided)
        {
            collision |= CollisionFlags.CEILING;
        }

        if(Trace(new Motion(Vector2.right, distance)).collided)
        {
            collision |= CollisionFlags.WALL_RIGHT;
        }

        if(Trace(new Motion(Vector2.left, distance)).collided)
        {
            collision |= CollisionFlags.WALL_LEFT;
        }

        _velocityInternal = velocity;
        _transform.position = position;
    }

    private void GetComponentsOnObject()
    {
        _transform = GetComponent<Transform>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void InitializeValues()
    {
        _bounds = _collider2D.bounds;
        _bounds.Expand(new Vector3(2 * -skinWidth, 2 * -skinWidth, 0));

        velocity = Vector2.zero;
        position = _transform.position;
    }

    public TraceResult MoveAndCollide(Motion motion)
    {
        var result = Trace(motion);
        position += motion.direction * result.travel;
        return result;
    }


    public void MoveAndSlide() {
        MoveAndSlide(new Motion(velocity.normalized, velocity.magnitude * Time.deltaTime));
    }

    /**
     * Move the object and slide along any collider in its path
     */
    public void MoveAndSlide(Motion motion)
    {
        for(var i = 0; i < MAX_SLIDES; i++)
        {
            var result = MoveAndCollide(motion);
            if(result.collided)
            {
                if(Mathf.Approximately(result.remainder, 0))
                {
                    velocity = Vector2.zero;
                    return;
                }
                motion = result.GetResponseMotion();
            }
            else
            {
                velocity = motion.direction * (motion.distance / Time.deltaTime);
                return;
            }
        }

        velocity = Vector2.zero;
    }

    public TraceResult Trace(Motion motion)
    {
        var results = new RaycastHit2D[1];
        var hits = Physics2D.BoxCast(position, _bounds.size, 0, motion.direction, contactFilter, results, motion.distance + skinWidth);
        if(hits > 0)
        {
            var normal      = results[0].normal;
            var dot         = Vector2.Dot(motion.direction, normal);
            var penetration = dot * skinWidth;
            var travel      = Vector2.Distance(position, results[0].centroid) + penetration;
            var remainder   = (motion.distance - travel) * (1 + dot);
            var projection  = (motion.direction - normal * dot).normalized;
            return new TraceResult(true, normal, penetration, travel, remainder, projection);
        }
        else
        {
            return new TraceResult(false, Vector2.zero, 0, motion.distance, 0, motion.direction);
        }
    }

    public bool Overlaps(Vector2 position)
    {
        var results = new Collider2D[4];
        var amount = Physics2D.OverlapBox(position, _bounds.size, 0, contactFilter, results);
        return amount > 0;
    }

    public SnapShot CaptureState()
    {
        return new SnapShot(
            _transform.position,
            _velocityInternal,
            collision
        );
    }

    public void RestoreState(SnapShot snapShot)
    {
        position = snapShot.position;
        velocity = snapShot.velocity;
        collision = snapShot.collision;
        Debug.Log("state restored");
    }

    public bool OnFloor()
    {
        return collision.HasFlag(CollisionFlags.FLOOR);
    }

    public bool OnWall()
    {
        return collision.HasFlag(CollisionFlags.WALL_RIGHT | CollisionFlags.WALL_LEFT);
    }
}