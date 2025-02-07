using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrrigerTalk : MonoBehaviour
{
    //主人公がトリガーに触れたら一度だけイベント
    [SerializeField] private UnityEvent playEv;

    private bool saw = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Human") && saw == false){
            saw = true;
            playEv.Invoke();
        }
    }
}
