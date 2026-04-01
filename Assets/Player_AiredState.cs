using UnityEngine;

public class Player_AiredState : EntityState
{
    public Player_AiredState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.doubleJumpReady && input.Player.Jump.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.doubleJumpState);
        }

        if (player.movementInput.x != 0 && !player.isDashing)
        {
            player.SetVelocity(player.movementInput.x * player.moveSpeed * player.inAirMoveMultiplier, rb.linearVelocityY);
        }
    }
}
