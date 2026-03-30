using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickAxeState : EntityState
{
    public PlayerPickAxeState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        anim.SetBool("PickAxe",true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        HandlePickAxeVelocity();

        if (pickAxeTriggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
        
        player.SetLastMove(player.lastMoveDir.x,player.lastMoveDir.y);
    }

    public override void Exit()
    {
        base.Exit();

        anim.SetBool("PickAxe",false);
    }

    private void HandlePickAxeVelocity()
    {
        player.SetVelocity(0,0);
    }
}
