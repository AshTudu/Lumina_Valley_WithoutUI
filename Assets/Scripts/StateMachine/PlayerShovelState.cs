using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShovelState : EntityState
{
    public PlayerShovelState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        anim.SetBool("Shovel",true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (shovelTriggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }

        HandleShovelVelocity();

        player.SetLastMove(player.lastMoveDir.x,player.lastMoveDir.y);
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool("Shovel",false);
    }

    private void HandleShovelVelocity()
    {
        player.SetVelocity(0,0);
    }
}

