using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class TitleManager : MonoBehaviour
{
    //選ばれているメニューを起動するための所//
    
    [SerializeField] private GameObject MainCanvas = null;
    [SerializeField] private GameObject EndListCanvas = null;
    [SerializeField] private GameObject HowPlayCanvas = null;

    public int MenuNum = 0;

    private bool CanEnter = true;
    
    private SoundManager SoundMan;

    void Awake()
    {
        Debug.unityLogger.logEnabled = false;
        Application.targetFrameRate = 60; 
        SoundMan = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Serecting:" + MenuNum);
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)) && CanEnter){
            SoundMan.PlaySE(3);
            switch(MenuNum){
                //1 ~ 4 Title
                //5~
                
                case 1: //ゲーム開始
                    SceneManager.LoadScene("StageStart");
                    break;
                case 2: //エンディングリスト表示
                    EndListCanvas.SetActive(true);
                    MainCanvas.SetActive(false);
                    break;
                case 3: //あそびかた表示
                    HowPlayCanvas.SetActive(true);
                    MainCanvas.SetActive(false);
                    break;
                case 4: //ゲーム終了
                    #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
                    #else
                        Application.Quit();//ゲームプレイ終了
                    #endif
                    break;
            }
        }
    }

    public void SetEnter(bool TorF){
        CanEnter = TorF;
    }
}
