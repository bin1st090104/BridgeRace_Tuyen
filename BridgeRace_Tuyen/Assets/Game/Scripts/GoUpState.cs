using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoUpState : IState
{
    private Vector3 target = new Vector3(0, 20.5f, 80);
    public void OnEnter(Bot bot)
    {
        NavMeshAgent navMeshAgent = bot.GetComponent<NavMeshAgent>();
        navMeshAgent.destination = target;
    }
    public IState OnExcute(Bot bot)
    {
        if(bot.brickCount() == 0)
        {
            return new FindState();
        }
        return this;
    }
    public void OnExit(Bot bot)
    {
        NavMeshAgent navMeshAgent = bot.GetComponent<NavMeshAgent>();
        navMeshAgent.destination = bot.transform.position;
    }
}

