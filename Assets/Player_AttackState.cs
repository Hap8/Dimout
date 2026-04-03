using UnityEngine;

public class Player_AttackState : EntityState
{
    private PlayerAttackRange attackRange;
    private float attackTimer;

    public Player_AttackState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
        attackRange = player.AttackRange;
    }

    public override void Enter()
    {
        base.Enter();

        player.isAttacking = true;
        attackTimer = player.attackDuration;

        attackRange.gameObject.SetActive(true);
        attackRange.UpdatePosition();
    }

    public override void Update()
    {
        base.Update();

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.isAttacking = false;

        attackRange.gameObject.SetActive(false);
    }
}
