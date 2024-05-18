using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFollowerController", menuName = "Controllers/FollowerController")]
public class FollowerControllerSO : ControllerSO
{
    public float FollowerDelay;

    private Pawn leader;
    Vector2 lastLeaderVelocity = Vector2.zero;
    Vector2 lastVelocity = Vector2.zero;
    private float timer = 0.0f;
    private int myPathIndex;

    public override ControllerSO Init(Pawn myPawn) {
        ControllerSO controller = CloneGeneric<FollowerControllerSO>();
        controller.SetOwnerPawn(myPawn);
        return controller;
    }

    /// <summary>
    /// The following overrides the functionality from the base. In this controller, the follower will attempt to follow another pawn
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <returns></returns>
    public override void FixedUpdate()
    {
        if (IsFrozen || leader == null) return;

        Debug.Log("attempting to access: " + myPathIndex);
        myPawn.transform.position = leader.FollowerPath[myPathIndex - 1];
    }

    public void SetLeader(Pawn leader)
    {
        leader.RegisterFollower(this);

        myPathIndex = leader.FollowerDistance * leader.Followers.Count;
        lastVelocity = Vector2.zero;
        // the controller can now be moved
        this.leader = leader;
    }
}