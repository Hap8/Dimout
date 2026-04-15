using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Rigidbody2D rb { get; protected set; }
    protected StateMachine stateMachine;

    [Header("Entity Movement")]
    public int facingDir = 1;

    [Header("Collision")]
    [SerializeField] protected float groundCheckDistance = 0.5f;
    [SerializeField] protected float wallCheckDistance = 0.5f;
    [SerializeField] protected LayerMask groundLayer;

    public bool groundDetected { get; protected set; }
    public bool wallDetected { get; protected set; }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
    }

    protected virtual void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }

    protected void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        wallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, wallCheckDistance, groundLayer);
    }

    public void SetVelocity(float x, float y)
    {
        rb.linearVelocity = new Vector2(x, y);
    }

    public void Flip()
    {
        facingDir *= -1;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * facingDir * wallCheckDistance);
    }
}
