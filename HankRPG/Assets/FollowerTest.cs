using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerTest : MonoBehaviour
{
    [SerializeField] private Transform _myLeader;
    [SerializeField] private int _queueIndex = 10;

    private Vector2[] leaderPositions = new Vector2[100];
    private Vector2 lastPosition = Vector2.zero;

    private void Start()
    {
        for (int i = leaderPositions.Length - 1; i >= 0; i--) {
            leaderPositions[i] = _myLeader.transform.position;
        }

        lastPosition = _myLeader.transform.position;
    }

    void FixedUpdate()
    {
        Vector2 currentPos = _myLeader.transform.position;
        if (currentPos == lastPosition) return;

        for (int i = leaderPositions.Length - 1; i > 0; i--) {
            leaderPositions[i] = leaderPositions[i - 1];
        }

        leaderPositions[0] = currentPos;
        lastPosition = currentPos;

        // update current position
        transform.position = leaderPositions[_queueIndex];
    }
}
