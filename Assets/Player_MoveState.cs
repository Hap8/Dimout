using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if(player.movementInput.x == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if(!player.isDashing)
        {
            player.SetVelocity(player.movementInput.x * player.moveSpeed, rb.linearVelocityY);
        }
    }
}
