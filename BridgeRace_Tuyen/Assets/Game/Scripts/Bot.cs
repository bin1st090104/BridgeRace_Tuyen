using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    private List<GameObject> brickStack = new List<GameObject>();

    public int brickCount()
    {
        return brickStack.Count;
    }
    public void AddBrick(GameObject brick)
    {
        brick.transform.SetParent(transform);
        brick.transform.localPosition = new Vector3(0f, 1 + brickStack.Count * 0.5f, -0.5f);
        brick.transform.localRotation = Quaternion.Euler(Vector3.zero);
        //Debug.Log(brick.transform.localRotation + "   " + brick.transform.localRotation);
        brickStack.Add(brick);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent != null)
        {
            GameObject obj = other.gameObject.transform.parent.gameObject;
            if(obj.tag == "Character")
            {
                Player player = obj.GetComponent<Player>();
                if(brickCount() > player.brickCount())
                {

                }
            }
            else
            if(obj.tag == "Bot")
            {

            }
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
