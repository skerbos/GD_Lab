using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleGameEvent", menuName = "ScriptableObjects/SimpleGameEvent", order = 3)]
public abstract class Variable<T> : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    protected T _value;
    public T Value
    {
        get { return _value; }
        set { SetValue(value);  }
    }

    public abstract void SetValue(T value);
}
