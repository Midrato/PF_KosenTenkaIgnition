using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PlayerController Human;
    [SerializeField] private GameObject PauseWindow;
    
    void Update()
    {
        if(Human.CanAct && Input.GetKeyDown(KeyCode.Escape) && !(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space))){
            //主人公が動けるかつ、他の動作を起こそうとしていないときにポーズメニューを開ける
            PauseWindow.SetActive(true);
        }

        if(PauseWindow.activeInHierarchy){
            Human.CanAct = false;
        }
    }
}
