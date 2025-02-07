using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrovalVariableManager : MonoBehaviour
{   
    [Header("ゲーム完成時これは削除 0=false,1=trueとして使用")]
    [SerializeField] private int GetEnd1 = 0;
    [SerializeField] private int GetEnd2 = 0;
    [SerializeField] private int GetEnd3 = 0;
    [SerializeField] private int GetEnd4 = 0;
    [SerializeField] private int GetEnd5 = 0;
    [SerializeField] private int GetEndex = 0;

    void Awake()
    {
        //Debug.Log(GetEnd1);
        PlayerPrefs.SetInt("GetEnd1", GetEnd1);
        PlayerPrefs.SetInt("GetEnd2", GetEnd2);
        PlayerPrefs.SetInt("GetEnd3", GetEnd3);
        PlayerPrefs.SetInt("GetEnd4", GetEnd4);
        PlayerPrefs.SetInt("GetEnd5", GetEnd5);
        PlayerPrefs.SetInt("GetEndex", GetEndex);

        Application.targetFrameRate = 60;
    }
}
