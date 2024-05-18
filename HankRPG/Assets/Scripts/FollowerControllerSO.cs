using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFollowerController", menuName = "Controllers/FollowerController")]
public class FollowerControllerSO : ControllerSO
{
    public float FollowerDelay;

    private Queue<LeaderLandmark> landmarks = new Queue<LeaderLandmark>();
    private Pawn leader;

    Vector2 lastLeaderVelocity = Vector2.zero;
    Vector2 lastVelocity = Vector2.zero;
    private float timer = 0.0f;
    private float myDelay = 0.0f;

    public override ControllerSO Clone() => CloneGeneric<FollowerControllerSO>();

    /// <summary>
    /// The following overrides the functionality from the base. In this controller, the follower will attempt to follow another pawn
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <returns></returns>
    public override ControllerCommand GetControllerCommand(float deltaTime)
    {
        ControllerCommand command = new ControllerCommand();
        if (IsFrozen || leader == null) return command;

        // dont move when leader isnt moving
        if (leader.MyRigidbody.velocity == Vector2.zero) return command;
        else timer += deltaTime;

        UpdateLandmarks();

        if (landmarks.TryPeek(out LeaderLandmark landmark) && timer >= landmark.RelativeTimestamp) {
            lastVelocity = landmarks.Dequeue().LeaderVelocity;
        }

        command.DesiredVelocity = lastVelocity;
        return command;
    }

    /// <summary>
    /// Updates the landmarks based on the leaders current velocity. 
    /// </summary>
    private void UpdateLandmarks()
    {
        Vector2 currentVel = leader.MyRigidbody.velocity;

        // record timestamp
        if (lastLeaderVelocity != currentVel) {
            landmarks.Enqueue(new LeaderLandmark(leader.MyRigidbody.velocity, timer + myDelay));
        }

        // record velocity
        lastLeaderVelocity = currentVel;
    }

    public void SetLeader(Pawn leader)
    {
        leader.RegisterFollower(this);

        myDelay = FollowerDelay;
        lastVelocity = Vector2.zero;
        // the controller can now be moved
        this.leader = leader;
    }
}

public struct LeaderLandmark
{
    public Vector2 LeaderVelocity;
    public float RelativeTimestamp; // based on the leaders internal follower timer

    public LeaderLandmark(Vector2 leaderVelocity, float relativeTimestamp)
    {
        LeaderVelocity = leaderVelocity;
        RelativeTimestamp = relativeTimestamp;
    }
}
