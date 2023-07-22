using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Anim" && other.transform.parent.name == "Player") 
        {
            UIManager.Ins.OpenUI<Victory>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
