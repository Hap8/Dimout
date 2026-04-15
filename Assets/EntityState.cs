using UnityEngine;

public abstract class EntityState
{
    protected Entity entity;
    protected StateMachine stateMachine;
    protected string stateName;
    protected float stateTimer;

    public EntityState(Entity entity, StateMachine stateMachine, string stateName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}


