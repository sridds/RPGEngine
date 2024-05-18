using UnityEngine;

[CreateAssetMenu(fileName = "NewInputController", menuName = "Controllers/InputController")]
public class InputControllerSO : ControllerSO
{
    public float BaseMovementSpeed;
    public float RunSpeed;

    public override ControllerSO Clone() => CloneGeneric<InputControllerSO>();

    public override ControllerCommand GetControllerCommand(float deltaTime)
    {
        if (IsFrozen) return new ControllerCommand();

        ControllerCommand myCommand = new ControllerCommand();
        myCommand.DesiredVelocity.x = Input.GetAxisRaw("Horizontal");
        myCommand.DesiredVelocity.y = Input.GetAxisRaw("Vertical");

        // get speed based on whether or not player is running
        float speed = Input.GetKey(KeyCode.X) ? RunSpeed : BaseMovementSpeed;

        // override desired vel
        myCommand.DesiredVelocity = myCommand.DesiredVelocity.normalized * speed;

        return myCommand;
    }
}
