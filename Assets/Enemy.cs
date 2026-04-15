using UnityEngine;

public class Enemy : Entity
{
    public float patrolSpeed;
    public float chaseSpeed;
    public float playerDetectDistance;
    public LayerMask playerLayer;
    [SerializeField] private Vector2 bounceBackForce;
    [SerializeField] private float turnCooldown = 0.5f;
    private float turnCooldownTimer = 0f;

    public EnemyPatrolState patrolState { get; private set; }
    public EnemyChaseState chaseState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        patrolState = new EnemyPatrolState(this, stateMachine, "patrol");
        chaseState = new EnemyChaseState(this, stateMachine, "chase");
    }

    private void Start()
    {
        stateMachine.Initialize(patrolState);
    }

    protected override void Update()
    {
        base.Update();

        if (turnCooldownTimer > 0f)
            turnCooldownTimer -= Time.deltaTime;
    }

    public bool PlayerDetected()
    {
        return Physics2D.Raycast(transform.position, Vector2.right * facingDir, playerDetectDistance, playerLayer);
    }

    public bool LedgeDetected()
    {
        Vector2 origin = (Vector2)transform.position + (Vector2)transform.right * 0.3f;
        return Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, groundLayer);
    }

    public bool WallDetected()
    {
        return Physics2D.Raycast(transform.position, transform.right, wallCheckDistance, groundLayer);
    }

    public void BounceBack()
    {
        rb.linearVelocity = new Vector2(-facingDir * bounceBackForce.x, bounceBackForce.y);
    }

    public bool CanTurn()
    {
        return turnCooldownTimer <= 0f;
    }

    public void ResetTurnCooldown()
    {
        turnCooldownTimer = turnCooldown;
    }

    
}

