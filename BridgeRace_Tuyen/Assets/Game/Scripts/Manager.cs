using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] bot;
   
    private void Start()
    {
        for(int i = 0; i < bot.Length; ++i)
        {
            bot[i].GetComponent<NavMeshAgent>().SetDestination(new Vector3(Random.Range(0, 14), 0, Random.Range(0, 14)));
        }
    }
}
