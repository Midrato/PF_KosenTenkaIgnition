using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public event Action OnEventTriggered;

    public void TriggerEvent()
    {
        // イベントを発火
        OnEventTriggered?.Invoke();
    }
}
