using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TalkEventMaker : MonoBehaviour
{
    [SerializeField] private List<UnityEvent> TalkEvent = new List<UnityEvent>();
    [Space]
    [TextArea(1, 3)]
    [SerializeField] private List<string> TalkContent = new List<string>();
    [SerializeField] private List<Sprite> TalkSprite = new List<Sprite>();

    [Space]
    [SerializeField] private Message MessageManager;

    [SerializeField] private GameObject BGImage;

    [Space]
    [Header("ランダムで出る文字列・イベント")]
    [TextArea(1, 3)]
    [SerializeField] private List<string> Randomstrings = new List<string>();
    [SerializeField] private List<UnityEvent> RandomEvents = new List<UnityEvent>();

    
    private Image _BGImage;
    
    private PlayerController Player;

    private bool IsEndEvent = false;
    
    private void Start() {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        _BGImage = BGImage.GetComponent<Image>();
    }
    
    public void PlayEvent(){
        StartCoroutine(_PlayEvent());
    }

    private IEnumerator _PlayEvent(){   //リストの上から順に設定されたイベント実行、イベント終わるまでは次のイベントに入らない
        for(int i = 0;i < TalkEvent.Count;i++){
            Player.CanAct = false;
            IsEndEvent = false;
            TalkEvent[i].Invoke();
            Player.CanAct = false;
            while(!IsEndEvent){
                Player.CanAct = false;
                yield return null; 
            }
            Player.CanAct = false;
        }
        Player.CanAct = true;
        Debug.Log("EventEndFree");
    }

    public void EndSignal(){    //イベント終了のシグナルを送る。Invoke用
        IsEndEvent = true;
    }

    public void StopMessage(){  //メッセージ止める
        MessageManager.stopMessage();
    }

    /*public void TimetoNext(float time){

    }*/

    public IEnumerator EnterToNextEv(float CanEndFirst){
        yield return new WaitForSeconds(CanEndFirst);   //保障待ち時間
        while(!Input.anyKeyDown){  //なにかキーを押したら進行
            yield return null;
        }
        EndSignal();
    }

    public void SendstrMessage(int StringNum){ //メッセージを送る
        //Player.CanAct = false;
        MessageManager.SetMessagePanel(TalkContent[StringNum]);
        StartCoroutine(waitEndMessage());
    }

    private IEnumerator waitEndMessage(){
        yield return null;
        while(!MessageManager.isEndMessage){   //会話終わりまで待つ
            yield return null;
        }
        EndSignal();
    }

    public void waitTime(float time){
        Invoke("EndSignal", time);
    }


    public void ShowImage(int ImageNum){    //リストImageNum番の画像を表示。フェードアウトと合わせる
        StartCoroutine(_shouIm(ImageNum));
        Invoke("EndSignal", 0.6f);
    }

    public void ShowImEnter(int ImageNum){    //リストImageNum番の画像を表示。何か押さないと次に行かない
        StartCoroutine(_shouIm(ImageNum));
        StartCoroutine(EnterToNextEv(0.6f));
    }

    private IEnumerator _shouIm(int ImNum){    //Invoke用
        yield return new WaitForSeconds(0.3f);
        BGImage.SetActive(true);
        _BGImage.sprite = TalkSprite[ImNum];
    }

    public void EndImage(){ //表示している画像を閉じる。
        Invoke("_endIm", 0.3f);
        Invoke("EndSignal", 0.6f);
    }

    private void _endIm(){
        BGImage.SetActive(false);
    }

    public void SendMesRandom(){    //ランダムメッセージ
        int n = Random.Range(0, Randomstrings.Count);   //リストの要素数以下から乱数生成
        MessageManager.SetMessagePanel(Randomstrings[n]); //選ばれたメッセージを表示
        StartCoroutine(waitEndMessage());
    }
    public void MesandEvRandom(){    //ランダムメッセージ
        int n = Random.Range(0, Randomstrings.Count);   //リストの要素数以下から乱数生成
        MessageManager.SetMessagePanel(Randomstrings[n]); //選ばれたメッセージを表示
        RandomEvents[n].Invoke();
        StartCoroutine(waitEndMessage());
    }
}
