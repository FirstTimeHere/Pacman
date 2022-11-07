using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 8f;
    public float speedMultipier = 1f;
    public Vector2 initialDirection;
    public LayerMask obstacleOrWallLayer;
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; } //
    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.startingPosition = this.transform.position;
    }
    private void Start()
    {
        ResetState();
    }
    public void ResetState()
    {
        this.speedMultipier = 1f;
        this.direction = this.initialDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startingPosition;
        this.rigidbody.isKinematic = false;
        this.enabled = true;
    }
    private void FixedUpdate()
    {
        Vector2 position = this.rigidbody.position;
        Vector2 translation = this.direction * this.speed * this.speedMultipier * Time.fixedDeltaTime;
        this.rigidbody.MovePosition(position+translation);
    }
    public void SetDirection(Vector2 direction, bool forced=false)
    {
        if (forced||!Occupied(direction))
        {
            this.direction = direction;
            this.nextDirection = Vector2.zero;

        }
        else
        {
            this.nextDirection= direction;
        }
    }
    public bool Occupied(Vector2 direction)
    {
        //луч проверяет что не можем мы пройти сквозь стену
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0f, direction, 1.5f, this.obstacleOrWallLayer);
        return hit.collider!=null;
    }
    private void Update()
    {
        if (this.nextDirection!=Vector2.zero)
            SetDirection(this.nextDirection);
    }
}
