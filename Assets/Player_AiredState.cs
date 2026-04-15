using UnityEngine;

public class Player_AiredState : PlayerState
{
    public Player_AiredState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.doubleJumpReady && !player.isDashing && input.Player.Jump.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.doubleJumpState);
        }

        if (player.movementInput.x != 0 && !player.isDashing)
        {
            player.SetVelocity(player.movementInput.x * player.moveSpeed * player.inAirMoveMultiplier, rb.linearVelocityY);
        }
    }
}
