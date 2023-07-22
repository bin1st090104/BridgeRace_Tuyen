using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 shiftPosition;
    [SerializeField] private Vector3 cameraRotation;
    void Start()
    {
        gameObject.transform.rotation = Quaternion.Euler(cameraRotation);
    }
    // Update is called once per frame

    private Player player;
    void Update()
    {
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }
        if(player == null)
        {
            return; 
        }
        transform.position = player.transform.position + shiftPosition;
        Debug.Log(player.transform.position + "   " + transform.position);
    }
}
