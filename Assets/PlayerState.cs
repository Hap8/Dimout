using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;
    protected PlayerInputSet input;
    protected Rigidbody2D rb;

    public PlayerState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
        this.player = player;
        this.input = player.input;
        this.rb = player.rb;
    }

    public override void Update()
    {
        base.Update();

        // DASH
        if (player.dashReady && !player.isDashing && input.Player.Dash.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.dashState);
            return;
        }

        // ATTACK
        if (!player.isAttacking && input.Player.Attack.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.attackState);
            return;
        }

        // WALL SLIDE
        if (!player.groundDetected && player.wallDetected && rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.wallSlideState);
            return;
        }

        // PARRY
        if (!player.isParrying && player.parryReady && !player.isAttacking && input.Player.Parry.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.parryState);
            return;
        }
    }
}

