using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Color currentColor;
    private IntPair pos;

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
        if (other.gameObject.transform.parent != null)
        {
            GameObject obj = other.gameObject.transform.parent.gameObject;
            if (obj.tag == "Character")
            {
                //Debug.Log("[1]" + gameObject.name + "   " + obj.name);
                if (other.gameObject.GetComponent<Anim>().GetColor() == currentColor)
                {
                    //Debug.Log("[2]" + gameObject.name + "   " + obj.name);
                    PlayerPicked(gameObject, obj);
                }
            }
            else
            if(obj.tag == "Bot")
            {
                if (other.gameObject.GetComponent<Anim>().GetColor() == currentColor)
                {
                    BotPicked(gameObject, obj);
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
