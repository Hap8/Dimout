using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName) { }

    public override void Update()
    {
        base.Update();

        if (enemy.WallDetected() && enemy.CanTurn())
        {
            enemy.BounceBack();
            enemy.Flip();
            enemy.ResetTurnCooldown();

            stateMachine.ChangeState(enemy.patrolState);
            return;
        }

        enemy.SetVelocity(enemy.facingDir * enemy.chaseSpeed, enemy.rb.linearVelocity.y);
    }

}

