using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;

    protected Animator anim;
    protected bool triggerCalled;
    protected bool axeTriggerCalled;
    protected bool pickAxeTriggerCalled;
    protected bool shovelTriggerCalled;
    protected bool wateringTriggerCalled;

    public EntityState(Player player, StateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;

        anim = player.anim;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        axeTriggerCalled = false;
        pickAxeTriggerCalled = false;
        shovelTriggerCalled = false;
        wateringTriggerCalled = false;
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void Exit()
    {

    }

    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }

    public void CallAxeTrigger()
    {
        axeTriggerCalled = true;
    }
    
    public void CallPickAxeTrigger()
    {
        pickAxeTriggerCalled = true;
    }

    public void CallShovelTrigger()
    {
        shovelTriggerCalled = true;
    }

    public void CallWateringTrigger()
    {
        wateringTriggerCalled = true;
    }
}
