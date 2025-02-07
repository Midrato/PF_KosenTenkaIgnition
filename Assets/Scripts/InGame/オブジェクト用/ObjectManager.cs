using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject OnCursol;

    [HideInInspector] public Message MessageMan = null;

    [Header("改ページ:<> イベント:<>_ev")]
    [SerializeField]
    [TextArea(1, 20)]
    protected string EventString = "";

    public bool SendMes = false;
    
    private bool onHuman = false;

    private PlayerController player;

    private SoundManager SoundMan;  //音用
    private bool isFirst = true;    //はじめから判定内にいたら鳴らさない
    
    protected virtual void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        MessageMan = GameObject.Find("MessageWindow").GetComponent<Message>();
        SoundMan = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        //if(MessageMan != null){Debug.Log("Find MessageMan");}
        Invoke("isntFirst", 0.1f);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        OnCursol.SetActive(onHuman);
        if(onHuman && player.CanAct && GloValues.NowAct > 0){   //動けない状態・行動回数0での行動をゆるすな
            if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)) && !SendMes){
                //Debug.Log("On and Do");
                SendMes = true;
                MessageMan.SetMessagePanel(EventString);
            }
        }
        if(MessageMan.isEndMessage){
            SendMes = false;
        }
    }

    public void OnActiveHumanEnter(){
        //Debug.Log("On Human");
        if(!isFirst && !onHuman){
                SoundMan.PlaySE(7);
                isFirst = false;
        }
        onHuman = true;
    }

    public void OnHumanExit(){
        onHuman = false;
    }

    private void isntFirst(){   //最初ではないことを確定
        isFirst = false;
    }
}
