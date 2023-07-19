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
    void Update()
    {
        GameObject player = MySingleton<Player>.Instance.gameObject;
        transform.position = player.transform.position + shiftPosition;
    }
}
