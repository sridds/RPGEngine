using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Pawn : MonoBehaviour
{
    [SerializeField] private ControllerSO _myController;

    public ControllerSO MyController { get { return _myController; } }
    public Rigidbody2D MyRigidbody { get { return _rb; } }
    public readonly List<FollowerControllerSO> Followers = new List<FollowerControllerSO>();

    private Rigidbody2D _rb;
    private ControllerCommand _currentFrameCommand;

    private void Awake()
    {
        // setup rigidbody
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _rb.freezeRotation = true;

        // clone the controller to ensure no values remain
        _myController = _myController.Clone();
    }

    private void FixedUpdate()
    {
        if (_myController == null) return;

        // Update velocity based on controller request
        _currentFrameCommand = _myController.GetControllerCommand(Time.fixedDeltaTime);
        _rb.velocity = _currentFrameCommand.DesiredVelocity;
    }

    public void RegisterFollower(FollowerControllerSO follower)
    {
        Followers.Add(follower);
    }

    public bool DeregisterFollower(FollowerControllerSO follower)
    {
        if(Followers.FirstOrDefault(x => x.Equals(follower)))
        {
            Followers.Remove(follower);
            return true;
        }

        return false;
    }

    public void SetController(ControllerSO newController) => _myController = newController.Clone();
}
