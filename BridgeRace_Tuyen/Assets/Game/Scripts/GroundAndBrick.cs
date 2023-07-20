using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct IntPair
{
    public int first;
    public int second;
    public IntPair(int first, int second)
    {
        this.first = first;
        this.second = second;
    }
}

public class GroundAndBrick : MonoBehaviour
{
    public static Dictionary<Color, int> orderOfColor = new Dictionary<Color, int>()
    {
        [Color.Blue] = 0,
        [Color.Green] = 1,
        [Color.Purple] = 2,
        [Color.Red] = 3,
        [Color.Yellow] = 4
    };
    public static Color[] brickColor = { Color.Blue, Color.Green, Color.Purple, Color.Red, Color.Yellow };
    private GameObject[,] brick;
    [SerializeField] GameObject brickPrefab;
    private int[] countColor = new int[5];
    private Queue<IntPair> positionNotHaveBrick;
    private List<Transform>[] brickWithColor;
    public void PickBrick(IntPair pos)
    {
        GameObject pickedBrick = brick[pos.first + 5, pos.second + 5];
        positionNotHaveBrick.Enqueue(pos);
        Color currentColor = pickedBrick.GetComponent<Brick>().GetCurrentColor();
        --countColor[orderOfColor[currentColor]];
        brickWithColor[orderOfColor[currentColor]].Remove(pickedBrick.transform);
        //Destroy(pickedBrick);
    }
    private int nextColor()
    {
        int c;
        do
        {
            c = Random.Range(0, 5);
        }
        while (countColor[c] >= 26);
        return c;
    }
    private IEnumerator AutoInstantiateBrick()
    {
        while (true)
        {
            while (positionNotHaveBrick.Count > 0)
            {
                IntPair pos = positionNotHaveBrick.Dequeue();
                brick[pos.first + 5, pos.second + 5] = Instantiate(brickPrefab, transform);
                brick[pos.first + 5, pos.second + 5].GetComponent<Brick>().SetPos(new IntPair(pos.first, pos.second));
                brick[pos.first + 5, pos.second + 5].transform.localPosition = new Vector3(pos.first * 2, brick[pos.first + 5, pos.second + 5].transform.localPosition.y, pos.second * 2);
                int c = nextColor();
                brick[pos.first + 5, pos.second + 5].GetComponent<Brick>().ChangColor(brickColor[c]);
                brick[pos.first + 5, pos.second + 5].GetComponent<Brick>().SetOnGround(true);
                ++countColor[c];
                brickWithColor[c].Add(brick[pos.first + 5, pos.second + 5].transform);
                yield return new WaitForSeconds(5f);
            }
            yield return new WaitForSeconds(5f);
        }
    }
    private void Awake()
    {
        brickWithColor = new List<Transform>[5];
        for(int i = 0; i < 5; ++i)
        {
            brickWithColor[i] = new List<Transform>();
        }
    }
    void Start()
    {
        brick = new GameObject[11, 11];
        positionNotHaveBrick = new Queue<IntPair>();
        for(int x = -5; x <= 5; ++x)
        {
            for(int z = -5; z <= 5; ++z)
            {
                brick[x + 5, z + 5] = Instantiate(brickPrefab, transform);
                brick[x + 5, z + 5].GetComponent<Brick>().SetPos(new IntPair(x, z));
                brick[x + 5, z + 5].transform.localPosition = new Vector3(x * 2, brick[x + 5, z + 5].transform.localPosition.y, z * 2);
                int c = nextColor();
                brick[x + 5, z + 5].GetComponent<Brick>().ChangColor(brickColor[c]);
                brick[x + 5, z + 5].GetComponent<Brick>().SetOnGround(true);
                ++countColor[c];
                brickWithColor[c].Add(brick[x + 5, z + 5].transform);
            }
        }
        StartCoroutine(AutoInstantiateBrick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
