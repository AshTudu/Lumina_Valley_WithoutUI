using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : EntityState
{
    public PlayerMoveState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        anim.SetBool("Walk",true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.moveInput.x == 0 && player.moveInput.y == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (player.isSprinting)
        {
            stateMachine.ChangeState(player.sprintState);
        }

        if (player.isAttacking)
        {
            stateMachine.ChangeState(player.basicAttackState);
        }

        player.SetVelocity(player.moveInput.x * player.moveSpeed , player.moveInput.y * player.moveSpeed);
        player.SetLastMove(player.moveInput.x, player.moveInput.y);
    }

    public override void Exit()
    {
        base.Exit();

        anim.SetBool("Walk",false);
    }
}
