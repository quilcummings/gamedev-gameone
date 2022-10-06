using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;

    // have camera follow player gameobject
    void Update()
    {
        transform.position = new Vector3(transform.position.x, target.position.y, -10);
    }
}
