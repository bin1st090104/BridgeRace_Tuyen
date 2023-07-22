using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private float speed;
    [SerializeField] private new Rigidbody rigidbody;

    //[SerializeField] bool onStair = false;
    //[SerializeField] private GameObject stair;
    private List<GameObject> brickStack = new();
    private int floor;
    private GameObject currentGroundAndBrick;

    public void DropBrick()
    {
        for(int i = 0; i < brickStack.Count; ++i)
        {
            StartCoroutine(brickStack[i].GetComponent<Brick>().DropToGround(currentGroundAndBrick));
        }
        brickStack.Clear();
    }
    public int brickCount()
    {
        return brickStack.Count;
    }
    private IEnumerator AddGravityForce()
    {
        while (true)
        {
            rigidbody.AddForce(0, -1, 0);
            yield return new WaitForSeconds(0.2f);
        }
    }
    public bool CanGetBrick()
    {
        if(brickStack.Count == 0)
        {
            return false;
        }
        GameObject brick = brickStack[brickStack.Count - 1];
        brickStack.RemoveAt(brickStack.Count - 1);
        Destroy(brick);
        return true;
    }
    public MyColor GetColor()
    {
        return GetComponentInChildren<Anim>().GetColor();
    }
    public void AddBrick(GameObject brick)
    {
        brick.transform.SetParent(transform);
        brick.transform.localPosition = new Vector3(0f, 1 + brickStack.Count * 0.5f, -0.5f);
        brick.transform.localRotation = Quaternion.Euler(Vector3.zero);
        brickStack.Add(brick);
    }
    private void OnTriggerEnter(Collider other)
    {
 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Door")
        {
            Debug.Log("trigger exit " + other.tag);
        }
        if (other.tag == "Door")
        {
            //GetComponentInChildren<Anim>().SetLayer("Default");
            other.isTrigger = false;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Door")
        {
            Debug.Log(name + " col enter Door " + floor + "<=>" + collision.gameObject.GetComponent<Door>().floor);
            if(floor < collision.gameObject.GetComponent<Door>().floor)
            {
                //GetComponentInChildren<Anim>().SetLayer("CanThroughDoor");
                collision.gameObject.GetComponent<Collider>().isTrigger = true;
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log("Col Stay " + collision.gameObject.name);
    }
    public void OnCollisionExit(Collision collision)
    {
        //Debug.Log("col exit " + collision.gameObject.tag);
        //if (collision.gameObject.tag == "Door" && GetComponentInChildren<Anim>().GetLayer() == "CanThroughDoor")
        //{
        //    GetComponentInChildren<Anim>().SetLayer("Default");
        //}
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Control();
    }
    private Vector3 firstMousePosition = new Vector3(0f, 0f, 0f);
    private Vector3 direct;
    private bool isMoving; 
    void Control()
    {
        direct = new Vector3(0f, 0f, 0f);
        isMoving = false;
        if (Input.GetMouseButtonDown(0))
        {
            firstMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            direct =  Input.mousePosition - firstMousePosition;
            if (direct != Vector3.zero)
            {
                direct.z = direct.y;
                direct.y = 0f;
                //direct = (direct / direct.magnitude) * speed;
                isMoving = true;
            }
            if (isMoving)
            {
                transform.LookAt(direct);
                Move(direct);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            }
        }
    }
    [SerializeField] private LayerMask layerMask;
    private void Move(Vector3 dir)
    {
        Vector3 nextPosition = Vector3.MoveTowards(transform.position, transform.position + direct, speed * Time.deltaTime);
        RaycastHit hit;
        //Debug.DrawRay(nextPosition + Vector3.up, Vector3.down * 2, UnityEngine.Color.yellow, 5f);
        if(Physics.Raycast(nextPosition + Vector3.up, Vector3.down, out hit, 2f, layerMask))
        {
            if (hit.transform.name == "Ground")
            {
                floor = hit.transform.gameObject.GetComponent<Ground>().floor;
                currentGroundAndBrick = hit.transform.parent.gameObject;
            }
            else
            if (hit.transform.name == "Stair")
            {
                //Debug.Log("hit Stair");
                nextPosition = hit.point + new Vector3(0f, 0.25f, 0f);
            }
            //Debug.Log("hit ");
        }
        else
        {
            //Debug.Log("not hit");
        }
        transform.position = nextPosition;
    }
    private void FixedUpdate()
    {
    }
}