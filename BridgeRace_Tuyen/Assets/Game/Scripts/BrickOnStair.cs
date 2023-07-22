using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickOnStair : MonoBehaviour
{
    // Start is called before the first frame update
    private MyColor curentColor;
    private bool hasColor = false;
    private bool isTrigger = false;
    [SerializeField] private BoxCollider boxCollider;
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(gameObject.name + "<--->" + collision.gameObject.name);
        if(collision.gameObject.tag == "Character")
        {
            if (collision.gameObject.name == "Player") {
                Player player = collision.gameObject.GetComponent<Player>();
                if(hasColor && curentColor == player.GetColor())
                {
                    boxCollider.isTrigger = true;
                    isTrigger = true;
                }
                else
                if (/*(hasColor || curentColor != player.GetColor()) &&*/ player.CanGetBrick())
                {
                    CurrentColor = player.GetColor();
                    boxCollider.isTrigger = true;
                    isTrigger = true;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Anim" && other.transform.parent != null)
        {
            GameObject obj = other.transform.parent.gameObject;
            if(obj.name == "Bot")
            {
                if (!hasColor || curentColor != obj.GetComponent<Bot>().GetColor()) {
                    if(obj.GetComponent<Bot>().CanGetBrick())
                    {
                        CurrentColor = obj.GetComponent<Bot>().GetColor();
                    }
                }
            }
        }
    }
    private IEnumerator SetTrigger(bool value, float time)
    {
        yield return new WaitForSeconds(time);
        boxCollider.isTrigger = value;
        isTrigger = value;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Character")
        {
            if(other.gameObject.name == "Player")
            {
                if(isTrigger)
                {
                    StartCoroutine(SetTrigger(false, 0.3f));
                }
            }
        }
    }
    public MyColor CurrentColor
    {
        get
        {
            return curentColor;
        }
        set
        {
            hasColor = true;
            curentColor = value;
            ColorMaterials.ChangColor(gameObject, value);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
