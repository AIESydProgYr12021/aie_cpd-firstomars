using System;

public abstract class OneByOneAttribute : Attribute
{ }

public abstract class ValidationAttribute : OneByOneAttribute
{
    public abstract bool Validate(System.Reflection.FieldInfo field, UnityEngine.Object instance);
    public abstract string ErrorMessage { get; }
}