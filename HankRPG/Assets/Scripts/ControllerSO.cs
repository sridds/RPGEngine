using UnityEngine;
using System;
using System.Reflection;

public abstract class ControllerSO : ScriptableObject
{
    protected bool IsFrozen;
    protected Pawn myPawn;

    public virtual void Update() { }
    public virtual void FixedUpdate() { }

    // Sets the frozen state of the controller. Inheriting types may expand on this functionality
    public virtual void SetFrozenState(bool isFrozen) => IsFrozen = isFrozen;

    // This should be called on deconstruction to ensure variables set during runtime are not kept
    public virtual void ResetToDefault() => IsFrozen = false;

    public void SetOwnerPawn(Pawn myPawn) => this.myPawn = myPawn;

    // this method must be overwritten by inheriting types to call the CloneGeneric method, passing in the type of item it is
    public abstract ControllerSO Init(Pawn myPawn);

    #region Helpers
    /// <summary>
    /// Handles the cloning of a specified item inheritor
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected ControllerSO CloneGeneric<T>() where T : ControllerSO
    {
        T Instance = ScriptableObject.CreateInstance<T>();
        Instance = CopyValuesReflection(Instance);

        return Instance;
    }

    /// <summary>
    /// Copies values inside the scriptable object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Instance"></param>
    /// <returns></returns>
    protected T CopyValuesReflection<T>(T Instance) where T : ControllerSO
    {
        Type type = typeof(T);
        foreach (FieldInfo field in type.GetFields())
        {
            field.SetValue(Instance, field.GetValue(this));
        }

        return Instance;
    }
    #endregion
}