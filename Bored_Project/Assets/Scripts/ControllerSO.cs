using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerSO : ScriptableObject
{
    [Min(0)] public float MovementSpeed;

    private bool IsFrozen;

    // Returns a controller command based on the inheriting controller
    public abstract ControllerCommand GetControllerCommand(float deltaTime);

    // Sets the frozen state of the controller. Inheriting types may expand upon this functionality
    public virtual void SetFrozenState(bool isFrozen) => IsFrozen = isFrozen;

    // This should be called on deconstruction to ensure any variables set during runtime are not kept.
    public virtual void ResetToDefault() => IsFrozen = false;
}

public struct ControllerCommand
{
    public Vector2 DesiredInput;
}
