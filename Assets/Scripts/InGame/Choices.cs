using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choices : MonoBehaviour
{
    //文字列のリストを受け取り、上下で選択できる選択肢を作成。
    //選択された物によって、int形のやつの中身を変え選択されたフラグon
    //フラグonならint形の中身を渡し、フラグoffにするメソッド実行してもらって元に戻る。

    [SerializeField] private GameObject ChoiceWindow;
    private RectTransform _ChoiceWindow;
    private Vector2[] ChoiceWindowSaver = new Vector2[2];

    [SerializeField] private GameObject ChoicePrefab;
    
    //[SerializeField] private float WindowUpDefault = 660;
    [SerializeField] private float WindowUpValue = -60;

    //選択肢の制御スクリプトをぶん回す。何度も消されたり加えられたりするかわいそうな子
    private List<cho_selecting> ChoControl = new List<cho_selecting>();

    //一時的に作成したプレハブのオブジェクトを保存
    private GameObject tmp = null;

    private int nowChoice = 0;
    private int result = -1; //デフォルト-1

    public bool isActive = false;

    private SoundManager SoundMan;  //音入れ


    void Start()
    {
        _ChoiceWindow = ChoiceWindow.GetComponent<RectTransform>();
        SoundMan = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        ChoiceWindowSaver[0] = _ChoiceWindow.sizeDelta;
        ChoiceWindowSaver[1] = _ChoiceWindow.position;
        ChoiceWindow.SetActive(false);
    }

    private void Update() {
        if(ChoControl.Count != 0 && result == -1){

            if(Input.GetKeyDown(KeyCode.UpArrow)){
                Debug.Log("choiceUp");
                
                if(nowChoice > 0){
                    nowChoice--;
                }else{
                    nowChoice = ChoControl.Count - 1;   //一番上で上押したら一番下に
                }
                
                SoundMan.PlaySE(1);
                ChoImSetActive(nowChoice);
            }else if(Input.GetKeyDown(KeyCode.DownArrow)){
                Debug.Log("choiceDown");

                if(nowChoice < ChoControl.Count - 1){
                    nowChoice++;
                }else{
                    nowChoice = 0;  //一番下で下押したら一番上に
                }
                
                SoundMan.PlaySE(1);
                ChoImSetActive(nowChoice);
            }else if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)){
                Debug.Log("choiceSelect");
                result = nowChoice;
                if(nowChoice < ChoControl.Count){
                    SoundMan.PlaySE(3);
                }else{
                    SoundMan.PlaySE(5);
                }
            }else if(Input.GetKeyDown(KeyCode.X)){
                Debug.Log("CancelSelect");
                result = ChoControl.Count - 1;
                SoundMan.PlaySE(5);
            }
            
        }
    }

    public void CreateChoice(List<string> strList){
        isActive = true;

        ChoiceWindow.SetActive(true);

        //選択肢ウィンドウ画面の大きさ調整
        _ChoiceWindow.sizeDelta = new Vector2(_ChoiceWindow.sizeDelta.x, _ChoiceWindow.sizeDelta.y + (WindowUpValue * (strList.Count-1)));
        _ChoiceWindow.position = new Vector2(_ChoiceWindow.position.x, _ChoiceWindow.position.y + (WindowUpValue * (strList.Count-1) / 2));

        

        int j = strList.Count;
        for(int i=0;i < strList.Count;i++){
            j--;    //カウントダウンする

            //選択肢のプレハブから、定位置から変位だけ上の位置に生成。選択肢画面を親にする。一時的にtmpに保存
            tmp = Instantiate(ChoicePrefab) as GameObject;
            
            tmp.transform.SetParent(ChoiceWindow.transform, false);

            //選択肢を上に(リスト番号若い方が上)
            tmp.transform.position += new Vector3(0, WindowUpValue * j - (WindowUpValue * (strList.Count-1) / 2), 0);

            ChoControl.Add(tmp.GetComponent<cho_selecting>());
            ChoControl[i].SetString(strList[i]);
        }
        ChoImSetActive(nowChoice);
    }

    public void ClearChoice(){
        nowChoice = 0;  //選択肢一番上に
        result = -1;
        ChoControl.Clear(); //コンポーネント削除

        foreach (Transform child in ChoiceWindow.transform)
        {
            //選択肢画面の子供をDestroyする
            Destroy(child.gameObject);

        }
        
        isActive = false;
        _ChoiceWindow.sizeDelta = ChoiceWindowSaver[0];
        _ChoiceWindow.position = ChoiceWindowSaver[1];
        ChoiceWindow.SetActive(false);
    }

    /// <summary>
    /// 指定のリストのみチョイスにする
    /// </summary>
    /// <param name="choiceNum"></param> <summary>
    /// チョイスされたリストの番号
    /// </summary>
    /// <param name="choiceNum"></param>
    private void ChoImSetActive(int choiceNum){
        if(ChoControl.Count != 0){
            for(int i=0;i < ChoControl.Count;i++){
                ChoControl[i].isChoice = false;
            }
            ChoControl[choiceNum].isChoice = true;
        }
    }

    public int SendResult(){
        return result;
    }

}
