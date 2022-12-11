using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Transition
{
	NullTransition=0,
	SeePlayer,
	LostPlayer,
	ClosePlayer
}
public enum StateID
{
	NullStateID=0,
	Patrol,
	Chase,
	Attack
}

/// <summary>
/// 状态的基类
/// </summary>
public abstract class FSMState{

	protected StateID stateID;//此时状态的id
	public StateID ID { get { return stateID; } }
	//管理状态的库 键是过渡条件 值是状态
	protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();
	protected FSMSystem fsm;//自己的中介

	public FSMState(FSMSystem fsm)
	{
		this.fsm = fsm;
	}

	/// <summary>
	/// 添加过度条件
	/// </summary>
	/// <param name="trans"></param>
	/// <param name="id"></param>
	public void AddTransition(Transition trans,StateID id)
	{
		if (trans == Transition.NullTransition)
		{
			Debug.LogError("不允许NullTransition");return;
		}
		if (id == StateID.NullStateID)
		{
			Debug.LogError("不允许NullStateID"); return;
		}
		if (map.ContainsKey(trans))
		{
			Debug.LogError("添加转换条件的时候，" + trans + "已经存在于map中");return;
		}
		map.Add(trans, id);
	}
	/// <summary>
	/// 删除过渡条件
	/// </summary>
	/// <param name="trans"></param>
	public void DeleteTransition(Transition trans)
	{
		if (trans == Transition.NullTransition)
		{
			Debug.LogError("不允许NullTransition"); return;
		}
		if (map.ContainsKey(trans)==false)
		{
			Debug.LogError("删除转换条件的时候，" + trans + "不存在于map中"); return;
		}
		map.Remove(trans);
	}
	/// <summary>
	/// 通过过渡条件获取状态的Id
	/// </summary>
	/// <param name="trans"></param>
	/// <returns></returns>
	public StateID GetOutputState(Transition trans)
	{
		if (map.ContainsKey(trans))
		{
			return map[trans];
		}
		return StateID.NullStateID;
	}
	/// <summary>
	/// 进入状态前的初始化
	/// </summary>
	public virtual void DoBeforeEntering() { }
	/// <summary>
	/// 离开此状态的清除
	/// </summary>
	public virtual void DoAfterLeaving() { }
	/// <summary>
	/// 实现的具体的逻辑
	/// </summary>
	/// <param name="npc"></param>
	public abstract void Act(GameObject npc);
	/// <summary>
	/// 转换状态的条件判断
	/// </summary>
	/// <param name="npc"></param>
	public abstract void Reason(GameObject npc);
}
