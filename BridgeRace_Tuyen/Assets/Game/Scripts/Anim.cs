using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    // Start is called before the first frame update
    private Color myColor;
    static private bool[] usedColor = new bool[5] { false, true, false, false, false};
    private void PrintBoolArray(bool[] array)
    {
        string result = "";
        for (int i = 0; i < array.Length; i++)
        {
            result += array[i].ToString() + " ";
        }

        Debug.Log(result);
    }

    public Color GetColor()
    {
        return myColor;
    }
    void Start()
    {
        if (transform.parent.gameObject.name == "Player")
        {
            myColor = GroundAndBrick.brickColor[1];
        }
        else
        {
            for(int i = 0; i < 5; ++i)
            {
                if (!usedColor[i])
                {
                    myColor = GroundAndBrick.brickColor[i];
                    usedColor[i] = true;
                    break;
                }
            }
        }
        //Debug.Log("Anim myColor:" + myColor);
        GetComponent<Renderer>().material = ColorMaterials.materials[(int)myColor];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
