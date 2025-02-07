using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndDayEv : MonoBehaviour
{
    [SerializeField] private List<UnityEvent> EndIvent = new List<UnityEvent>{null, null, null};

    private PlayerController Player = null;

    private bool IsEndDay = false;
    
    private void Start() {
        Player = this.gameObject.GetComponent<PlayerController>();
        IsEndDay = false;
    }

    void Update()
    {
        if(!IsEndDay){    //シーン遷移後、終日イベ
            IsEndDay = true;

            if(GloValues.NowDay == 3 && GloValues.SabotageExam){
                EndIvent[GloValues.NowDay].Invoke();    //試験欠席
            }else{
                EndIvent[GloValues.NowDay-1].Invoke();  //毎日の終了イベント
            }
        }
    }
}
