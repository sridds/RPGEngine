using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Pawn : MonoBehaviour
{
    [SerializeField] private ControllerSO _myController;
    [SerializeField] private int _followerDistance = 20;

    // - Follower Variables -
    public readonly List<FollowerControllerSO> Followers = new List<FollowerControllerSO>();
    public readonly List<Vector2> FollowerPath = new List<Vector2>();
    private int maxFollowerQueue = 0;

    // - Getters -
    public ControllerSO MyController { get { return _myController; } }
    public Rigidbody2D MyRigidbody { get { return rb; } }
    public int FollowerDistance { get { return _followerDistance; } }

    // - Local Variables -
    private Rigidbody2D rb;
    private Vector2 lastPosition;

    private void Awake()
    {
        // setup rigidbody
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        // clone the controller to ensure no values remain
        _myController = _myController.Init(this);
    }

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (_myController == null) return;
        _myController.Update();
    }

    private void FixedUpdate()
    {
        if (_myController == null) return;
        _myController.FixedUpdate();
        ShiftFollowerList();
    }

    /// <summary>
    /// The following function handles the bulk of the party follow system. By shifting an array of points of where the leader has been, other party members can access certain indices of the path as they continue to be shifted
    /// </summary>
    private void ShiftFollowerList()
    {
        Vector2 currentPos = transform.position;

        if (currentPos == lastPosition || FollowerPath.Count == 0) return;

        // This is responsible for shifting the entire array as the pawn continues to move
        for (int i = FollowerPath.Count - 1; i > 0; i--)
        {
            FollowerPath[i] = FollowerPath[i - 1];
        }

        // Set the new position and record last position
        FollowerPath[0] = currentPos;
        lastPosition = currentPos;
    }

    public void RegisterFollower(FollowerControllerSO follower)
    {
        Followers.Add(follower);

        int target = (_followerDistance * Followers.Count) - FollowerPath.Count;
        // Only allocate the necessary amount of spaces for the followers
        for (int i = 0; i < target; i++) {
            FollowerPath.Add(transform.position);
        }
    }

    public bool DeregisterFollower(FollowerControllerSO follower)
    {
        if(Followers.FirstOrDefault(x => x.Equals(follower)))
        {
            Followers.Remove(follower);
            // insert deallocation here.
            return true;
        }

        return false;
    }

    public void SetController(ControllerSO newController) => _myController = newController.Init(this);
}
