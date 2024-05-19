using UnityEngine;

[CreateAssetMenu(fileName = "NewInputController", menuName = "Controllers/InputController")]
public class InputControllerSO : ControllerSO
{
    public float BaseMovementSpeed;
    public float RunSpeed;

    public override ControllerSO Init(Pawn myPawn) {
        ControllerSO controller = CloneGeneric<InputControllerSO>();
        controller.SetOwnerPawn(myPawn);
        return controller;
    }
    
    public override void Update()
    {
        if (IsFrozen) return;

        Vector2 desiredVelocity = new Vector2();
        desiredVelocity.x = Input.GetAxisRaw("Horizontal");
        desiredVelocity.y = Input.GetAxisRaw("Vertical");

        // get speed based on whether or not player is running
        float speed = Input.GetKey(KeyCode.X) ? RunSpeed : BaseMovementSpeed;

        // update vel
        myPawn.MyRigidbody.velocity = desiredVelocity.normalized * speed;
    }
}
