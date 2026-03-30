using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxeState : EntityState
{
    public PlayerAxeState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        anim.SetBool("AxeAttack",true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        HandleAxeVelocity();

        if (axeTriggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
        
        player.SetLastMove(player.lastMoveDir.x,player.lastMoveDir.y);
    }

    public override void Exit()
    {
        base.Exit();

        anim.SetBool("AxeAttack", false);
    }

    private void HandleAxeVelocity()
    {
        player.SetVelocity(0, 0);
    }
}
