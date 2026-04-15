using UnityEngine;

public class Player_DashState : PlayerState
{
    public Player_DashState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
        player.dashReady = false;
        player.isDashing = true;

        player.SetVelocity(player.dashForce * player.facingDir, rb.linearVelocityY);
    }

    public override void Update()
    {
        base.Update();

        stateTimer -= Time.deltaTime;

        if (player.doubleJumpReady && !player.groundDetected && input.Player.Jump.WasPressedThisFrame())
        {
            Debug.Log("Double Jumping from Dash State");
            player.SetVelocity(rb.linearVelocityX / 2, rb.linearVelocityY);
            stateMachine.ChangeState(player.doubleJumpState);
        }

        if (player.groundDetected && input.Player.Jump.WasPressedThisFrame())
        {
            player.SetVelocity(rb.linearVelocityX / 2, rb.linearVelocityY);
            stateMachine.ChangeState(player.jumpState);
        }

        if (stateTimer < 0)
        {
            if (player.groundDetected)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                player.SetVelocity(rb.linearVelocityX / 2, rb.linearVelocityY);
                stateMachine.ChangeState(player.fallState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.isDashing = false;
    }
}
