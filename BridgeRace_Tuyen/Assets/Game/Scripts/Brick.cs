using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Color currentColor;
    private IntPair pos;
    private bool onGround;

    public IEnumerator DropToGround()
    {
        while (true)
        {
            if (onGround)
            {
                yield break;
            }
        }
    }
    public void SetOnGround(bool status)
    {
        onGround = status;
    }
    public Color GetCurrentColor()
    {
        return currentColor;
    }
    public IntPair GetPos()
    {
        return pos;
    }

    public void SetPos(IntPair pos)
    {
        this.pos = pos;
    }

    private void PlayerPicked(GameObject brick, GameObject player)
    {
        transform.parent.gameObject.GetComponent<GroundAndBrick>().PickBrick(pos);
        player.GetComponent<Player>().AddBrick(brick);
    }

    private void BotPicked(GameObject brick, GameObject player)
    {
        transform.parent.gameObject.GetComponent<GroundAndBrick>().PickBrick(pos);
        player.GetComponent<Bot>().AddBrick(brick);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(name + " trigger enter " + other.name);
        if (other.name == "Anim" && other.gameObject.transform.parent != null)
        {
            if (!onGround)
            {
                return;
            }
            GameObject obj = other.gameObject.transform.parent.gameObject;
            if (obj.tag == "Character")
            {
                //Debug.Log("[1]" + gameObject.name + "   " + obj.name);
                if (other.gameObject.GetComponent<Anim>().GetColor() == currentColor)
                {
                    //Debug.Log("[2]" + gameObject.name + "   " + obj.name);
                    PlayerPicked(gameObject, obj);
                    onGround = false;
                }
            }
            else
            if(obj.tag == "Bot")
            {
                if (other.gameObject.GetComponent<Anim>().GetColor() == currentColor)
                {
                    BotPicked(gameObject, obj);
                    onGround = false;
                }
            }
        }
    }
    public void ChangColor(Color c)
    {
        currentColor = c;
        GetComponent<Renderer>().material = ColorMaterials.materials[(int)c];
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
