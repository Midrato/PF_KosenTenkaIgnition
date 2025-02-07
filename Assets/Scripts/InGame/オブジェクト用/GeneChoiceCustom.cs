using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneChoiceCustom : MonoBehaviour
{   
    //選択肢
    public List<string> ChoiceTex = new List<string>();

    //選択後に表示するセリフ
    [SerializeField]
    [TextArea(1, 3)] 
    protected List<string> resultstr = new List<string>();

    //選択後に実行するメソッド
    [SerializeField] 
    protected List<UnityEvent> ResultEvent = new List<UnityEvent>();

    //有効化する選択肢(基本全部)
    [SerializeField] protected List<bool> enableChoice = new List<bool>{true};
    
    //リスト一時保存用
    protected List<string> tmpChoTex = new List<string>();
    protected List<string> tmpResTex = new List<string>();
    protected List<UnityEvent> tmpResEvent = new List<UnityEvent>();

    private ObjectManager ObjMan;
    private Message MessMan;
    private Choices ChoiceMan;

    private int restmp = -1;
    protected virtual void Start()
    {
        ObjMan = this.gameObject.GetComponent<ObjectManager>();
        MessMan = ObjMan.MessageMan;
        ChoiceMan = GameObject.Find("ChoiceManager").GetComponent<Choices>();
        ResetChoice();
    }

    
    protected virtual void Update()
    {
        if(ObjMan.SendMes && MessMan.HappenEvent){
            if(!ChoiceMan.isActive){
                Debug.Log("SendChoice!");
                ChoiceMan.CreateChoice(tmpChoTex);
                restmp = -1;
            }

            if(restmp != -1){ //選択が確定したら
                //メッセージマネージャー メッセージを終わらせ、イベントも終わらせ、結果に応じてセリフを送る
                MessMan.stopMessage();
                
                if(tmpResTex[restmp] != ""){
                    MessMan.SetMessagePanel(tmpResTex[restmp]);
                }

                //結果に応じて関数を実行する
                DoResult(restmp);
                
                //選択肢画面をクリア
                ChoiceMan.ClearChoice();
            }else{
                restmp = ChoiceMan.SendResult();
            }
        }
    }
    
    protected virtual void DoResult(int selectNum){
        if(tmpResEvent[selectNum] != null){
            tmpResEvent[selectNum].Invoke();
        }
    }

    /// <summary>
    /// 継承するとき楽できるやつ群
    /// </summary>
    public virtual void ResetChoice(){
        ClearTmp();
        //有効な選択肢なら一時選択肢に加える
        for(int i=0;i < ChoiceTex.Count;i++){
            if(enableChoice[i]){
                MakeTmp(i);
            }
        }
    }

    protected void MakeTmp(int MakeNum){
        tmpChoTex.Add(ChoiceTex[MakeNum]);
        tmpResTex.Add(resultstr[MakeNum]);
        tmpResEvent.Add(ResultEvent[MakeNum]);
    }

    protected void ClearTmp(){
        tmpChoTex.Clear();
        tmpResTex.Clear();
        tmpResEvent.Clear();
    }

    


    /// <summary>
    /// 継承するとき楽できるやつ 引数番目の選択肢を除去
    /// </summary>
    /// <param name="DelChoiceNum"></param>
    protected virtual void DelChoice(int DelChoiceNum){
        ChoiceTex.RemoveAt(DelChoiceNum);
        resultstr.RemoveAt(DelChoiceNum);
        ResultEvent.RemoveAt(DelChoiceNum);
    }
}
