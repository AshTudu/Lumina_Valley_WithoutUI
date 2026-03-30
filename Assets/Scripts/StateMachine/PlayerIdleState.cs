using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : EntityState
{
    public PlayerIdleState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        anim.SetBool("Idle",true);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.moveInput.x != 0 || player.moveInput.y != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }

        if (player.isSprinting && (player.moveInput.x != 0 || player.moveInput.y != 0))
        {
            stateMachine.ChangeState(player.sprintState);
        }
        
        if (player.input.Player.Attack.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(player.basicAttackState);
        }

        if (player.input.Player.TempAxe.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(player.axeState);
        }

        if (player.input.Player.TempPickAxe.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(player.pickAxeState);
        }

        if (player.input.Player.TempShovel.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(player.shovelState);
        }

        if (player.input.Player.TempWatering.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(player.wateringState);
        }

        player.SetLastMove(player.lastMoveDir.x,player.lastMoveDir.y);

    }

    public override void Exit()
    {
        base.Exit();

        anim.SetBool("Idle",false);
    }

    
}
