using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTester : MonoBehaviour
{
    [SerializeField]
    private Pawn leader;

    [SerializeField]
    private List<Pawn> follower = new List<Pawn>();

    private void Start()
    {
        Pawn last = leader;
        foreach(Pawn p in follower)
        {
            FollowerControllerSO follower = p.MyController as FollowerControllerSO;

            follower.SetLeader(last);

            last = p;
        }
    }
}
