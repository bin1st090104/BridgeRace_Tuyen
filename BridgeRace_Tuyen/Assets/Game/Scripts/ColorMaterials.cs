using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MyColor{
    Blue,
    Brown,
    Green,
    Grey,
    Pink,
    Purple,
    Red,
    White,
    Yellow
}
public class ColorMaterials : MonoBehaviour
{
    public static Material[] materials;
    public static int numColor;
    void Awake()
    {
        numColor = Enum.GetValues(typeof(MyColor)).Length;
        materials = new Material[numColor];
        int i = 0;
        foreach (MyColor value in Enum.GetValues(typeof(MyColor)))
        {
            //Debug.Log(value);
            materials[i] = Resources.Load<Material>("Materials/" + value);
            if (materials[i] == null)
            {
                //Debug.Log(value + "   fail:" + "Materials/" + value);
            }
            ++i;
        }
    }
    static public void ChangColor(GameObject gameObject, Material material) {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if(renderer == null)
        {
            return;
        }
        renderer.material = material;
        return;
    }
    static public void ChangColor(GameObject gameObject, MyColor color)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer == null)
        {
            return ;
        }
        renderer.material = materials[(int)color];
        return;
    }
}