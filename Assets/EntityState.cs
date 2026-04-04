using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string stateName;
    protected float stateTimer;

    protected Rigidbody2D rb;
    protected PlayerInputSet input;

    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateName = stateName;

        rb = player.rb;
        input = player.input;
    }

    public virtual void Enter()
    {

    }

    public virtual void Update()
    {
        if(player.dashReady && !player.isDashing && input.Player.Dash.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.dashState);
        }

        if(input.Player.Attack.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.attackState);
        }

        if(!player.groundDetected && player.wallDetected && player.rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }

    public virtual void Exit() 
    { 
    
    }
}
