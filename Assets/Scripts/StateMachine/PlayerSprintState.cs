using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintState : EntityState
{
    public PlayerSprintState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        anim.SetBool("isRunning",true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!player.isSprinting)
        {
            if (player.moveInput.x == 0 && player.moveInput.y == 0)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.moveState);
            }
        }

        player.SetVelocity(player.moveInput.x * player.sprintSpeed, player.moveInput.y * player.sprintSpeed);
        
        player.SetLastMove(player.moveInput.x, player.moveInput.y);
    }

    public override void Exit()
    {
        base.Exit();

        anim.SetBool("isRunning",false);
    }

}
