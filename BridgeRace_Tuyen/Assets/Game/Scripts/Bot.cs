using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    private List<GameObject> brickStack = new List<GameObject>();
    [SerializeField] private LayerMask layerMask;
    private GameObject currentGroundAndBrick;

    public bool CanGetBrick()
    {
        if (brickStack.Count == 0)
        {
            return false;
        }
        GameObject brick = brickStack[brickStack.Count - 1];
        brickStack.RemoveAt(brickStack.Count - 1);
        Destroy(brick);
        return true;
    }
    public GameObject GetCurrentGroundAndBrick()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 2, layerMask))
        {
            if (hit.transform.GetComponentInParent<GroundAndBrick>() != null)
            {
                return currentGroundAndBrick = hit.transform.parent.gameObject;
            }
        }
        return currentGroundAndBrick;
    }
    public void DropBrick()
    {
        for (int i = 0; i < brickStack.Count; ++i)
        {
            StartCoroutine(brickStack[i].GetComponent<Brick>().DropToGround(GetCurrentGroundAndBrick()));
        }
        brickStack.Clear();
    }
    public MyColor GetColor()
    {
        return GetComponentInChildren<Anim>().GetColor();
    }
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
