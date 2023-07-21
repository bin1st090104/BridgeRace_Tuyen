using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Color currentColor;
    private IntPair pos;
    private bool onGround;
    [SerializeField] float speedDrop;

    public IEnumerator DropToGround(GameObject groundAndBrick)
    {
        if(groundAndBrick == null)
        {
            Debug.Log("Aborted fail!!!");
            yield break;
        }
        ChangColor(Color.Grey);
        transform.SetParent(groundAndBrick.transform);
        Vector3 target = new Vector3(Random.Range(transform.localPosition.x - 3, transform.localPosition.x + 3)
                                    ,0.75f
                                    ,Random.Range(transform.localPosition.z - 3, transform.localPosition.z + 3));
        while (true)
        {
            if (Vector3.Distance(transform.localPosition, target) < 1e-3)
            {
                onGround = true;
                groundAndBrick.GetComponent<GroundAndBrick>().AddDropBrick(gameObject);
                yield break;
            }
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, speedDrop * Time.deltaTime);
            yield return null;
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
        if (currentColor == Color.Grey)
        {
            transform.GetComponentInParent<GroundAndBrick>().PickDropBrick(brick);
            brick.GetComponent<Brick>().ChangColor(player.GetComponent<Player>().GetColor());
        }
        else
        {
            transform.GetComponentInParent<GroundAndBrick>().PickBrick(pos);
        }
        player.GetComponent<Player>().AddBrick(brick);
    }

    private void BotPicked(GameObject brick, GameObject bot)
    {
        if (currentColor == Color.Grey)
        {
            transform.GetComponentInParent<GroundAndBrick>().PickDropBrick(brick);
            brick.GetComponent<Brick>().ChangColor(bot.GetComponent<Bot>().GetColor());
        }
        else
        {
            transform.GetComponentInParent<GroundAndBrick>().PickBrick(pos);
        }
        bot.GetComponent<Bot>().AddBrick(brick);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(name + " trigger enter " + other.name);
        if (other.name == "Anim" && other.gameObject.transform.parent != null)
        {
            if (!onGround)
            {
                return;
            }
            GameObject obj = other.gameObject.transform.parent.gameObject;
            if (obj.tag == "Character")
            {
                if (currentColor == Color.Grey || other.gameObject.GetComponent<Anim>().GetColor() == currentColor)
                {
                    PlayerPicked(gameObject, obj);
                    onGround = false;
                }
            }
            else
            if(obj.tag == "Bot")
            {
                if (currentColor == Color.Grey || other.gameObject.GetComponent<Anim>().GetColor() == currentColor)
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
