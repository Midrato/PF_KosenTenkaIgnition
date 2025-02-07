using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstStory : MonoBehaviour
{
    [SerializeField] private UnityEvent FirstEvent;

    void Start()
    {
        Invoke("PlayEv", 0.3f);
    }

    private void PlayEv(){
        FirstEvent.Invoke();
    }
}
