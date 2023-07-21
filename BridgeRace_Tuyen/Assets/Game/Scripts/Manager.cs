using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] bot;
    private IState[] currentState;

    private void Awake()
    {
        currentState = new IState[bot.Length];
    }
    private void ChangState(int i, IState newState)
    {
        if(currentState[i] != null)
        {
            currentState[i].OnExit(bot[i].GetComponent<Bot>());
        }
        currentState[i] = newState;
        currentState[i].OnEnter(bot[i].GetComponent<Bot>());
    }
    private void Start()
    {
        for(int i = 0; i < bot.Length; ++i)
        {
            ChangState(i, new FindState());
        }
    }
    private void Update()
    {
        for(int i = 0; i < bot.Length; ++i)
        {
            IState nextState = currentState[i].OnExcute(bot[i].GetComponent<Bot>());
            if(nextState != currentState[i])
            {
                ChangState(i, nextState);
            }
        }
    }
}
