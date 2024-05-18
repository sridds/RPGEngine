using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLerpToPos : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smoothTime = 0.5f;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, target.transform.position, Time.deltaTime * smoothTime);
    }
}
