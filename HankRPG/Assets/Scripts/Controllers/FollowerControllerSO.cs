using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFollowerController", menuName = "Controllers/FollowerController")]
public class FollowerControllerSO : ControllerSO
{
    private Pawn leader;
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

        // Access the position in the path
        myPawn.transform.position = leader.FollowerPath[myPathIndex];
    }

    public void SetLeader(Pawn leader)
    {
        leader.RegisterFollower(this);
        leader.OnFollowerRemoved += PartyMemberRemoved;

        // Since this was newley registered as a follower, we can just access Followers.Count without issue
        myPathIndex = (leader.FollowerDistance * leader.Followers.Count) - 1;
        // the controller can now be moved
        this.leader = leader;
    }

    private void PartyMemberRemoved(int index)
    {
        myPathIndex -= leader.FollowerDistance;
    }

    /// <summary>
    /// The following will ensure the proper reset of all data once the follower is removed from the party
    /// </summary>
    public void AbandonLeader()
    {
        leader.OnFollowerRemoved -= PartyMemberRemoved;
        leader.DeregisterFollower(this);
        leader = null;
        myPathIndex = 0;
    }
}