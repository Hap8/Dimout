using UnityEngine;

public class Player_WallSlideState : Player_GroundedState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.rb.gravityScale *= player.wallSlideSpeedMultiplier;
    }

    public override void Update()
    {
        base.Update();

        if (!player.wallDetected || player.groundDetected)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.rb.gravityScale /= player.wallSlideSpeedMultiplier;
    }
}
