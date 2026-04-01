using UnityEngine;

public class Player_AiredState : EntityState
{
    public Player_AiredState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();

        if(player.movementInput.x != 0)
        {
            player.SetVelocity(player.movementInput.x * player.moveSpeed * player.inAirMoveMultiplier, rb.linearVelocityY);
        }
    }
}
