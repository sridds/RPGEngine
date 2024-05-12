using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInputController", menuName = "Controllers/InputController")]
public class InputControllerSO : ControllerSO
{
    public override ControllerCommand GetControllerCommand(float deltaTime)
    {
        ControllerCommand myCommand = new ControllerCommand();

        myCommand.DesiredInput.x = Input.GetAxisRaw("Horizontal");
        myCommand.DesiredInput.y = Input.GetAxisRaw("Vertical");

        return myCommand;
    }
}
