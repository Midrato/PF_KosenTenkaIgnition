using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectManChild : MonoBehaviour
{
    [SerializeField] private UnityEvent OnActiveHuman;
    [SerializeField] private UnityEvent ExitHuman;
    
    private void Start() {
        
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Human")){
            OnActiveHuman.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Human")){
            ExitHuman.Invoke();
        }
    }
    

}
