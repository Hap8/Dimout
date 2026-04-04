using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }

    public PlayerInputSet input;
    private StateMachine stateMachine;

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_DoubleJumpState doubleJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_AttackState attackState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }


    [Header("Movement Details")]
    public float moveSpeed;
    public float jumpForce;
    public float doubleJumpForce;
    public float inAirMoveMultiplier;
    public float dashForce;
    public float dashDuration;
    public float wallSlideSpeedMultiplier;

    public bool doubleJumpReady = true;
    public bool dashReady = true;
    public bool isDashing = false;
    public bool isAttacking = false;
    public int facingDir = 1;
    public Vector2 movementInput { get; private set; }

    [Header("Collision Details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask groundLayer;
    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }

    [Header("Attack")]
    public float attackDuration = 0.2f;
    [SerializeField] private PlayerAttackRange attackRange;
    public PlayerAttackRange AttackRange => attackRange;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jump");
        fallState = new Player_FallState(this, stateMachine, "fall");
        doubleJumpState = new Player_DoubleJumpState(this, stateMachine, "doubleJump");
        dashState = new Player_DashState(this, stateMachine, "dash");
        attackState = new Player_AttackState(this, stateMachine, "attack");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => movementInput = Vector2.zero;
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
        HandleFlip(movementInput.x);
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
    }

    private void HandleFlip(float xVelocity)
    {
        if(xVelocity > 0 && facingDir == -1)
        {
            Flip();
        }
        else if(xVelocity < 0 && facingDir == 1)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDir *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        wallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir, wallCheckDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * facingDir, 0));
    }
}
