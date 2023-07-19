using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindState : IState
{
    // Start is called before the first frame update
    private GroundAndBrick currentGround;
    private bool isDestination = false;
    public void OnEnter(Bot bot)
    {
        RaycastHit hit;
        if(Physics.Raycast(bot.transform.position, Vector3.down, out hit, 1f))
        {
            currentGround = hit.collider.gameObject.GetComponent<GroundAndBrick>();
            if(currentGround == null)
            {
                Debug.Log("Cannot get ground");
            }
        }
        else
        {
            Debug.Log("Not hit");
        }
    }
    public void OnExcute(Bot bot)
    {
        NavMeshAgent botAgent = bot.gameObject.GetComponent<NavMeshAgent>();
        if (isDestination)
        {

        }
        else
        {

        }
    }
    public void OnExit(Bot bot)
    {

    }
}
