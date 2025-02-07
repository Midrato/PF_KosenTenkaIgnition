using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEnd_TurnDisp : MonoBehaviour
{
    [Range(1, 6), SerializeField] private int EndNum = 1;
    [Header("ON/OFFそれぞれ表示したいオブジェクト")]
    [SerializeField] private GameObject TtoDisp = null;
    [SerializeField] private GameObject FtoDisp = null;

    private int judge = 0;

    void Awake()
    {
        switch(EndNum){
            case 1:
                judge = PlayerPrefs.GetInt("GetEnd1", 0);
                break;
            case 2:
                judge = PlayerPrefs.GetInt("GetEnd2", 0);
                break;
            case 3:
                judge = PlayerPrefs.GetInt("GetEnd3", 0);
                break;
            case 4:
                judge = PlayerPrefs.GetInt("GetEnd4", 0);
                break;
            case 5:
                judge = PlayerPrefs.GetInt("GetEnd5", 0);
                break;
            case 6:
                judge = PlayerPrefs.GetInt("GetEndex", 0);
                break;
        }
        //Debug.Log(judge);
    }

    private void OnEnable() {
        if(judge != 0){
            TtoDisp.SetActive(true);
            FtoDisp.SetActive(false);
        }else{
            TtoDisp.SetActive(false);
            FtoDisp.SetActive(true);
        }
        
    }
}
