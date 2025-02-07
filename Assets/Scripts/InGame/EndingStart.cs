using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndingStart : MonoBehaviour
{
    [SerializeField] private List<UnityEvent> EndEvent = new List<UnityEvent>();

    void Start()
    {
        Invoke("PlayEnding", 0.3f);
    }

    private void PlayEnding(){
        EndEvent[GloValues.ThisGameEnd].Invoke();
    }
}