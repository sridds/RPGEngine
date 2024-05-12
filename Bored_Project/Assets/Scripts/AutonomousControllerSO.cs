using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAutonomousController", menuName = "Controllers/AutonomousController")]
public class AutonomousControllerSO : ControllerSO
{
    public override ControllerCommand GetControllerCommand(float deltaTime)
    {
        throw new System.NotImplementedException();
    }
}
