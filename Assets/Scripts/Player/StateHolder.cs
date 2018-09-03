using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateHolder : MonoBehaviour
{
    [System.Serializable]
    public class StateEvent : UnityEvent<string> {}

    public StateEvent onStateChanged;

    public string state { get; protected set; }

    void Awake()
    {
        if (onStateChanged == null)
            onStateChanged = new StateEvent();
    }

    public void SetState(string value)
    {
        state = value;
        onStateChanged.Invoke(state);
    }
}
