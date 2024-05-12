using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField] private ControllerSO _myController;

    public ControllerSO MyController { get { return _myController; } }

    public void SetController(ControllerSO newController) => _myController = newController;

    private void Update()
    {
        if (_myController == null) return;

        ControllerCommand command = _myController.GetControllerCommand(Time.deltaTime);
        Debug.Log(command.DesiredInput);
    }
}