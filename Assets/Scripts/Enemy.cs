using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private FSMSystem fsm;

	void Start () {
		InitFSM();
	}

	void InitFSM()
	{
		fsm = new FSMSystem();

		FSMState patrolState = new PatrolState(fsm);
		patrolState.AddTransition(Transition.SeePlayer, StateID.Chase);

		FSMState chaseState = new ChaseState(fsm);
		chaseState.AddTransition(Transition.LostPlayer, StateID.Patrol);
		chaseState.AddTransition(Transition.ClosePlayer, StateID.Attack);

		FSMState atkState = new AtkState(fsm);
		atkState.AddTransition(Transition.SeePlayer, StateID.Chase);

		fsm.AddState(patrolState);
		fsm.AddState(chaseState);
		fsm.AddState(atkState);
	}

	void Update () {
		fsm.Update(this.gameObject);
	}
}
