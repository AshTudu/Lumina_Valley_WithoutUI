using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttackState : EntityState
{

    private const int FirstComboIndex = 1;
    private int comboIndex = 1;
    private int combolimit = 2;

    private float lastAttackTime;

    public PlayerBasicAttackState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        ResetComboIndexIfNeeded();

        anim.SetInteger("BasicAttackIndex", comboIndex);
        anim.SetBool("BasicAttack", true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        HandleAttackVelocity();
        
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }

        player.SetLastMove(player.lastMoveDir.x,player.lastMoveDir.y);
    }

    public override void Exit()
    {
        base.Exit();

        comboIndex++;
        anim.SetBool("BasicAttack", false);

        lastAttackTime = Time.time;
    }

    private void HandleAttackVelocity()
    {
        player.SetVelocity(0,0);
    }

    private void ResetComboIndexIfNeeded()
    {
        if (Time.time > lastAttackTime + player.comboResetTime)
        {
            comboIndex = FirstComboIndex;
        }

        if (comboIndex > combolimit)
        {
            comboIndex = FirstComboIndex;
        }
    }
}
