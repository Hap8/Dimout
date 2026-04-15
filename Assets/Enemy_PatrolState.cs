using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    public EnemyPatrolState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName) { }

    public override void Update()
    {
        base.Update();

        if (enemy.PlayerDetected())
        {
            stateMachine.ChangeState(enemy.chaseState);
            return;
        }

        if (enemy.WallDetected() || !enemy.LedgeDetected() && enemy.CanTurn())
        {
            enemy.Flip();
            enemy.ResetTurnCooldown();
        }

        enemy.SetVelocity(enemy.facingDir * enemy.patrolSpeed, enemy.rb.linearVelocity.y);
    }
}
