using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    private Player player;


    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    private void CurrentStateTrigger()
    {
        player.CallAnimationTrigger();
    }

    private void AxeAnimTrigger()
    {
        player.CallAxeAnimTrigger();
    }

    private void PickAxeAnimTrigger()
    {
        player.CallPickAxeTriggerCalled();
    }

    private void ShovelAnimTrigger()
    {
        player.CallShovelTriggerCalled();
    }

    private void WateringAnimTrigger()
    {
        player.CallWateringTriggerCalled();
    }
}
