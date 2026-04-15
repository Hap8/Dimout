using UnityEngine;

public class Player : Entity
{
    public PlayerInputSet input;

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_DoubleJumpState doubleJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_AttackState attackState { get; private set; }
    public Player_ParryState parryState { get; private set; }
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
    public bool parryReady = true;
    public bool isParrying = false;

    public Vector2 movementInput { get; private set; }

    [Header("Attack")]
    public float attackDuration = 0.2f;
    [SerializeField] private PlayerAttackRange attackRange;
    public PlayerAttackRange AttackRange => attackRange;

    [Header("Parry")]
    public float parryDuration = 0.1f;
    public float parryCooldown = 0.5f;
    private float parryCooldownTimer;
    [SerializeField] private PlayerParryRange parryRange;
    public PlayerParryRange ParryRange => parryRange;

    protected override void Awake()
    {
        base.Awake();

        input = new PlayerInputSet();
        parryCooldownTimer = parryCooldown;

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jump");
        fallState = new Player_FallState(this, stateMachine, "fall");
        doubleJumpState = new Player_DoubleJumpState(this, stateMachine, "doubleJump");
        dashState = new Player_DashState(this, stateMachine, "dash");
        attackState = new Player_AttackState(this, stateMachine, "attack");
        parryState = new Player_ParryState(this, stateMachine, "parry");
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

    protected override void Update()
    {
        base.Update();
        HandleFlip(movementInput.x);
        HandleParryCooldown();
    }

    private void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0 && facingDir == -1) Flip();
        else if (xVelocity < 0 && facingDir == 1) Flip();
    }

    private void HandleParryCooldown()
    {
        if (!parryReady)
        {
            parryCooldownTimer -= Time.deltaTime;

            if (parryCooldownTimer <= 0f)
            {
                parryReady = true;
                parryCooldownTimer = parryCooldown;
            }
        }
    }
}
