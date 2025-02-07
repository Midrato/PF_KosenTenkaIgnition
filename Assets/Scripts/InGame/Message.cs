using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class Message : MonoBehaviour{

    //メッセージUI
    private TextMeshProUGUI messageText;
    [SerializeField] private TextMeshProUGUI talkerText;
    //表示するメッセージ
    [SerializeField]
    [TextArea(1, 20)]
    private string allMessage = null;

    //使用する分割文字列
    [SerializeField] private string splitString = "<>";
    //イベントを発生させる時の文字列
    [SerializeField] private string eventString = "_ev";
    //話し手の名前で区切る文字列
    [SerializeField] private string talkerString = "/t";

    //分割したメッセージ
    private string[] splitMessage;
    //分割したメッセージの何番目か
    private int messageNum;

    //テキストスピード
    [SerializeField] private float textSpeed = 0.05f;
    //経過時間
    private float elapsedTime = 0f;
    //今見ている文字番号
    private int nowTextNum = 0;

    //マウスクリックを促すアイコン
    [SerializeField] private GameObject clickIcon;
    private Image _clickIcon;
    //クリックアイコンの点滅秒数
    [SerializeField] private float clickFlashTime = 0.2f;

    //1回分のメッセージを表示したかどうか
    private bool isOneMessage = false;
    //メッセージをすべて表示したかどうか
    public bool isEndMessage = true;
    //イベントをを発生させるかどうか
    private bool isEvent = false;
    //イベントウィンドウ挿入のタイミングか
    public bool HappenEvent = false;
    
    //プレイヤースクリプト(動き止める用)
    [SerializeField] private PlayerController player;

    //会話送り音用
    private SoundManager SoundMan;

    void Start(){
        _clickIcon = clickIcon.GetComponent<Image>();
        _clickIcon.enabled = false;
        messageText = GetComponentInChildren<TextMeshProUGUI>();
        messageText.text = "";

        SoundMan = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Update(){
        //メッセージが終わっているか、メッセージがない場合はこれ以降何もしない
        if(isEndMessage || allMessage == null){
            this.gameObject.SetActive(false);
            return;
        }else{
            player.CanAct = false;
        }

        //1回に表示するメッセージを表示していない	
        if(!isOneMessage){
            int talkernum = splitMessage[messageNum].IndexOf(talkerString);
            if(talkernum != -1){    //話し手の情報が見つかったなら
                talkerText.text = splitMessage[messageNum].Substring(0, talkernum);  //話し手テキストに代入
                splitMessage[messageNum] = splitMessage[messageNum].Substring(talkernum + talkerString.Length); //話し手情報の部分を削除
            }

            if(splitMessage.Length != messageNum + 1){
                isEvent = (splitMessage[messageNum + 1] == eventString);  //次の文字列がイベント文字列かどうか判定
            }
            //テキスト表示時間を経過したらメッセージを追加
            if(elapsedTime >= textSpeed){
                messageText.text += splitMessage[messageNum][nowTextNum];

                SoundMan.PlaySE(6); //会話送り音

                nowTextNum++;
                elapsedTime = 0f;

                //メッセージを全部表示、または行数が最大数表示された
                if (nowTextNum >= splitMessage[messageNum].Length){
                    isOneMessage = true;
                }
            }
            elapsedTime += Time.deltaTime;

            //メッセージ表示中にzかx,スペースを押したら一括表示
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)){
                //ここまでに表示しているテキストに残りのメッセージを足す
                messageText.text += splitMessage[messageNum].Substring(nowTextNum);
                isOneMessage = true;
            }

        //1回に表示するメッセージを表示した、かつイベントが発生する
        }else if(isOneMessage && isEvent){
            HappenEvent = true;

        //1回に表示するメッセージを表示した
        }else{

            elapsedTime += Time.deltaTime;

            //クリックアイコンを点滅する時間を超えた時、反転させる
            if(elapsedTime >= clickFlashTime){
                _clickIcon.enabled = !_clickIcon.enabled;
                elapsedTime = 0f;
            }

            //Zかスペース,Xが押されたら次の文字表示処理
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)){
                nowTextNum = 0;
                messageNum++;
                messageText.text = "";
                _clickIcon.enabled = false;
                elapsedTime = 0f;
                isOneMessage = false;

                //メッセージが全部表示されていたらゲームオブジェクト自体の削除
                if(messageNum >= splitMessage.Length){
                    player.CanAct = true;
                    isEndMessage = true;
                    transform.GetChild(0).gameObject.SetActive(false);

                    Debug.Log("MesEndFree");
                }
            }
        }
    }
    //新しいメッセージを設定
    void SetMessage(string message){
        this.allMessage = message;
        //分割文字列で一回に表示するメッセージを分割する
        splitMessage = Regex.Split(allMessage, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        nowTextNum = 0;
        messageNum = 0;
        messageText.text = "";
        isOneMessage = false;
        isEndMessage = false;
        
        int talkernum = splitMessage[0].IndexOf(talkerString);
        if(talkernum != -1){    //話し手の情報が見つかったなら
            talkerText.text = splitMessage[0].Substring(0, talkernum);  //話し手テキストに代入
            splitMessage[0] = splitMessage[0].Substring(talkernum + talkerString.Length); //話し手情報の部分を削除
        }
    }

    //強制的にメッセージを終了
    public void stopMessage(){
        HappenEvent = false;
        isEvent = false;
        isEndMessage = true;
        transform.GetChild(0).gameObject.SetActive(false);
        player.CanAct = true;

        Debug.Log("MesStopFree");
    }

    //他のスクリプトから新しいメッセージを設定しUIをアクティブにする
    public void SetMessagePanel(string message){
        if((isEndMessage || allMessage == null)){
            player.CanAct = false;
            this.gameObject.SetActive(true);
            SetMessage(message);
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
