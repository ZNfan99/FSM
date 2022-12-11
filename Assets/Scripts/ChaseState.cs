using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 追逐
/// </summary>
public class ChaseState : FSMState
{
	private Transform playerTransform;
	public ChaseState(FSMSystem fsm) : base(fsm)
	{
		stateID = StateID.Chase;
		playerTransform = GameObject.Find("Player").transform;
	}

	public override void Act(GameObject npc)
	{
		npc.transform.LookAt(playerTransform.position);
		npc.transform.Translate(Vector3.forward * 2 * Time.deltaTime);
		npc.GetComponent<Animator>().SetBool("run", true);
	}

    public override void DoAfterLeaving()
    {
        
    }

    public override void Reason(GameObject npc)
	{
		if (Vector3.Distance(playerTransform.position, npc.transform.position) > 6)
		{
			npc.GetComponent<Animator>().SetBool("run", false);
			fsm.PerformTransition(Transition.LostPlayer);
		}
		if(Vector3.Distance(playerTransform.position, npc.transform.position) <= 2)
		{
            npc.GetComponent<Animator>().SetBool("run", false);
            fsm.PerformTransition(Transition.ClosePlayer);
        }
	}
}