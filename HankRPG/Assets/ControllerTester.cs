using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTester : MonoBehaviour
{
    [SerializeField]
    private Pawn leader;

    [SerializeField]
    private DialogueObject testDialogue;

    [SerializeField]
    private List<Pawn> follower = new List<Pawn>();

    private void Start()
    {
        foreach(Pawn p in follower)
        {
            FollowerControllerSO follower = p.MyController as FollowerControllerSO;

            follower.SetLeader(leader);
        }
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.J)) {
            GameObject gameObject = new GameObject();

            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
            SpriteRenderer spr = gameObject.AddComponent<SpriteRenderer>();

            spr.sprite = defaultSprite;
            spr.color = Random.ColorHSV();
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1);

            Pawn p = gameObject.AddComponent<Pawn>();
            p.SetController(follower[0].MyController);

            (p.MyController as FollowerControllerSO).SetLeader(leader);
        }*/

        if (Input.GetKeyDown(KeyCode.K))
        {
            (leader.Followers[0]).AbandonLeader();
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            FindObjectOfType<DialogueHandler>().QueueDialogue(testDialogue);
        }
    }
}
