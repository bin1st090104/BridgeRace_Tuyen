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

    public void DropBrick()
    {

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
    public Color GetColor()
    {
        return GetComponentInChildren<Anim>().GetColor();
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
        //Debug.Log("--->" + other.name);
    }
    private bool onStair = false;
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Door")
        {
            if(brickStack.Count > 0)
            {
                collision.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            }
        }
        if(collision.gameObject.name == "Stair")
        {
            onStair = true;
        }
        Debug.Log(gameObject.name + " OnCollisionEnter " + collision.gameObject.name);
    }
    public void OnCollisionStay(Collision collision)
    {
        Debug.Log(gameObject.name + " OnCollisionStay " + collision.gameObject.name);
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            bool trigger = collision.gameObject.GetComponent<BoxCollider>().isTrigger;
            trigger = trigger ? true : false;
        }
        if(collision.gameObject.name == "Stair")
        {
            onStair = false;
        }
    }
    void Start()
    {
        //StartCoroutine(AddGravityForce());
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
        Debug.DrawRay(nextPosition, Vector3.down * 10, UnityEngine.Color.green, 10f);
        if (Physics.Raycast(nextPosition, Vector3.down, out hit, 0.5f, layerMask))
        {
            Debug.Log("-->>" + hit.point + "   " + hit.distance + "   " + hit.transform.name);
            Debug.DrawRay(nextPosition, Vector3.down * 3, UnityEngine.Color.yellow, 10f);
            if (onStair)
            {
                nextPosition += hit.point + new Vector3(0f, 1.25f, 0f);
            }
            else
            {
                nextPosition += hit.point + new Vector3(0f, 1f, 0f);
            }
        }
        else
        {
            Debug.Log("  not hit");
            Debug.DrawRay(nextPosition, Vector3.down * 10, UnityEngine.Color.red, 5f);
        }
        //Debug.DrawRay(new Vector3(0, 0, 0), Vector3.up * 10, UnityEngine.Color.red, Mathf.Infinity);
        transform.position = nextPosition;
    }
    private void FixedUpdate()
    {
    }
}