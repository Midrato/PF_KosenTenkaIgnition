using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class skipOpening : MonoBehaviour
{
    [SerializeField] private UnityEvent SkipEv;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SkipEv.Invoke();
        }    
    }
}
