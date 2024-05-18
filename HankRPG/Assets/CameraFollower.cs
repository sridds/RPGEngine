using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float speed = 15.0f;

    [SerializeField]
    private Vector3 offset = new Vector3(0, 0, -10);

    private void LateUpdate()
    {
        Vector3 pos = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * speed);
        transform.position = new Vector3(pos.x, pos.y, offset.z);
    }
}
