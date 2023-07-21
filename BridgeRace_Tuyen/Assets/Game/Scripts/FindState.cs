using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindState : IState
{
    // Start is called before the first frame update
    private GroundAndBrick currentGroundAndBrick;
    private bool hasDestination = false;
    private int lim;
    public void OnEnter(Bot bot)
    {
        currentGroundAndBrick = bot.GetCurrentGroundAndBrick().GetComponent<GroundAndBrick>();
        if(currentGroundAndBrick == null)
        {
            Debug.Log("fail enter");
        }
        else{
            Debug.Log("succes enter");
        }
        lim = Random.Range(3, 7);
    }
    public IState OnExcute(Bot bot)
    {
        NavMeshAgent botAgent = bot.gameObject.GetComponent<NavMeshAgent>();
        if (hasDestination)
        {
            if (botAgent.remainingDistance < 1e-3)
            {
                hasDestination = false;
                if (bot.brickCount() >= lim)
                {
                    return new GoUpState();
                }
            }
        }
        else
        {
            Transform nextDestination = currentGroundAndBrick.GetAnObtainedBrick(bot.GetColor());
            if(nextDestination != null)
            {
                hasDestination = true;
                botAgent.destination = nextDestination.position;
            }
        }
        return this;
    }
    public void OnExit(Bot bot)
    {

    }
}
