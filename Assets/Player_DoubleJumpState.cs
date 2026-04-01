using UnityEngine;

public class Player_DoubleJumpState : Player_AiredState
{
    public Player_DoubleJumpState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.doubleJumpReady = false;

        player.SetVelocity(rb.linearVelocityX, player.doubleJumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocityY < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }
}
