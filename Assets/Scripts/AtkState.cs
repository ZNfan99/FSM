using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ¹¥»÷
/// </summary>
public class AtkState : FSMState
{
    private Transform playerTransform;

    public AtkState(FSMSystem fsm) : base(fsm)
    {
        stateID = StateID.Attack;
        playerTransform = GameObject.Find("Player").transform;
    }

    public override void Act(GameObject npc)
    {
        npc.transform.LookAt(playerTransform.position);
        npc.GetComponent<Animator>().SetBool("skill", true);
    }

    public override void Reason(GameObject npc)
    {
        if(Vector3.Distance(playerTransform.position, npc.transform.position) > 2)
        {
            npc.GetComponent<Animator>().SetBool("skill", false);
            fsm.PerformTransition(Transition.SeePlayer);
        }
    }
}
