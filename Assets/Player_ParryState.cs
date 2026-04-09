using UnityEngine;

public class Player_ParryState : EntityState
{
    private PlayerParryRange parryRange;
    private float parryTimer;

    public Player_ParryState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
        parryRange = player.ParryRange;
    }

    public override void Enter()
    {
        base.Enter();

        player.isParrying = true;
        player.parryReady = false;
        parryTimer = player.parryDuration;

        parryRange.gameObject.SetActive(true);
    }

    public override void Update()
    {
        base.Update();

        parryTimer -= Time.deltaTime;

        if (parryTimer <= 0f)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.isParrying = false;

        parryRange.gameObject.SetActive(false);
    }
}
