using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWateringState : EntityState
{


    public PlayerWateringState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        anim.SetBool("Watering",true);
    }

    override public void LogicUpdate()
    {
        if (wateringTriggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }

        HandleWateringVelocity();
        player.WaterTile();
        player.SetLastMove(player.lastMoveDir.x,player.lastMoveDir.y);
    }

    public override void Exit()
    {
        base.Exit();

        anim.SetBool("Watering",false);
    }

    private void HandleWateringVelocity()
    {
        player.SetVelocity(0,0);
    }
}
