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
    private List<GameObject> brickStack = new List<GameObject>();
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
        //Debug.Log(gameObject.name + " OnCollisionEnter " + collision.gameObject.name);
    }
    public void OnCollisionStay(Collision collision)
    {
        //if (collision.gameObject.name == "Stair")
        //{
        //    stair = collision.gameObject;
        //    onStair = true;
        //}
        //if (collision.gameObject.name == "Ground")
        //{
        //    onStair = false;
        //}
        //Debug.Log(gameObject.name + " OnCollisionStay " + collision.gameObject.name);
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
                //Vector3 nextPosition = Vector3.MoveTowards(transform.position, transform.position + direct, speed * Time.deltaTime);
                //transform.position = nextPosition;
                transform.LookAt(direct);
                AddVelocity(direct);
                transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            }
        }
        //transform.position = new Vector3(Mathf.Max(transform.position.x, -0.5f), transform.position.y, transform.position.z);
        //transform.position = new Vector3(Mathf.Min(transform.position.x, +0.5f), transform.position.y, transform.position.z);
    }
    private void AddVelocity(Vector3 dir)
    {
        if (onStair)
        {
            Debug.Log(nameof(onStair) + ":" + onStair);
            float h = dir.magnitude;
            float cosAlpha = Vector3.Dot(dir, new Vector3(0, 0, dir.z > 0 ? 1 : -1)) / h;
            float u = h * cosAlpha;
            //float v = Mathf.Sqrt(h * h - u * u);
            //rigidbody.velocity = (dir + new Vector3(0, (dir.z > 0 ? u : -u), 0)).normalized * speed;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + dir + new Vector3(0, (dir.z > 0 ? u : -u), 0), speed * Time.deltaTime);
            if (dir.z > 0)
            {
                //rigidbody.AddForce(Vector3.up * 9.81f);
            }
        }
        else
        {
            //rigidbody.velocity = dir.normalized * speed + new Vector3(0, rigidbody.velocity.y, 0);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        }
    }
    private void FixedUpdate()
    {
    }
}