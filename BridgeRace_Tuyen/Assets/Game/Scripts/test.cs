using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator ChangeColor()
    {
        Debug.Log(ColorMaterials.numColor);
        for(int i = 0; i < ColorMaterials.numColor; ++i)
        {
            ColorMaterials.ChangColor(gameObject, ColorMaterials.materials[i]);
            Debug.Log((Color)i);
            yield return new WaitForSeconds(2f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name + " OnCollisionEnter " + collision.gameObject.name);
    }

    void Start()
    {
        //StartCoroutine(nameof(ChangeColor));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
