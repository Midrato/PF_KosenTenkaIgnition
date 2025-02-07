using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndListManager : MonoBehaviour
{
    [SerializeField] private GameObject EndlistCanvas = null;
    [SerializeField] private GameObject TitleCanvas = null;
    
    [SerializeField] private GameObject PageText = null;

    [SerializeField] private List<GameObject> biglist = new List<GameObject> {null, null};

    [SerializeField] private List<GameObject> endlist1 = new List<GameObject> {null, null, null, null};
    [SerializeField] private List<GameObject> endlist2 = new List<GameObject> {null, null, null, null};

    [SerializeField] private int nowlist = 0;
    [SerializeField] private int selectnum = 3;


    private List<EndlistSelect> ELS1 = new List<EndlistSelect> {null, null, null, null};
    private List<EndlistSelect> ELS2 = new List<EndlistSelect> {null, null, null, null};

    private TextMeshProUGUI _pageText = null;

    private SoundManager SoundMan;

    void Awake()
    {
        for(int i=0;i<ELS1.Count;i++){
            ELS1[i] = endlist1[i].GetComponent<EndlistSelect>();
        }
        for(int i=0;i<ELS2.Count;i++){
            ELS2[i] = endlist2[i].GetComponent<EndlistSelect>();
        }
        _pageText = PageText.GetComponent<TextMeshProUGUI>();

        SoundMan = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private void OnEnable() {
        nowlist = 0;
        ChangeList(nowlist);
        SelectMenu(nowlist, selectnum);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            UpList(nowlist);  //リストのカーソルを上へ
            SelectMenu(nowlist, selectnum);

            SoundMan.PlaySE(1);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            DownList(nowlist);    //リストのカーソルを下へ
            SelectMenu(nowlist, selectnum);

            SoundMan.PlaySE(1);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            ResetSelectandEnter();
            nowlist = nowlist == 0 ? 1 : 0; //リストのページ切り替え
            SelectMenu(nowlist, selectnum);
            ChangeList(nowlist);
            SelectMenu(nowlist, selectnum);

            SoundMan.PlaySE(1);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ResetSelectandEnter();
            nowlist = nowlist == 0 ? 1 : 0; //リストのページ切り替え
            SelectMenu(nowlist, selectnum);
            ChangeList(nowlist);
            SelectMenu(nowlist, selectnum);

            SoundMan.PlaySE(1);
        }
        else if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
        {
            EnterMenu(nowlist, selectnum);
            if(selectnum == 3){
                ReturnTitle();
            }
            SoundMan.PlaySE(5);
        }
    }
    /// <summary>
    /// カーソルを上に移動させる。上の項目がないときはさらに上に移動
    /// </summary>
    /// <param name="nowlist"></param> <summary>
    /// 現在のリストのページ
    /// </summary>
    /// <param name="nowlist"></param>
    void UpList(int nowlist){
        if(nowlist == 0){
            if(selectnum != 0){
                selectnum--;
            }
            
            int tmpnum = selectnum; //一時的にselectnumを保管

            while(!endlist1[tmpnum].activeInHierarchy){  //一つ上が空欄だったら繰り返し
                //Debug.Log(endlist1[tmpnum].activeInHierarchy);
                tmpnum--;

                if(tmpnum < 0){ //0を下回ったから適用しない
                    tmpnum = selectnum + 1;
                    break;
                } 
            }
            selectnum = tmpnum;

        }else if(nowlist == 1){
            if(selectnum != 0){
                selectnum--;
            }
            
            int tmpnum = selectnum; //一時的にselectnumを保管
            
            while(!endlist2[tmpnum].activeInHierarchy){  //一つ上が空欄だったら繰り返し
                //Debug.Log(endlist2[tmpnum].activeInHierarchy);
                tmpnum--;

                if(tmpnum < 0){ //0を下回ったから適用しない、元に戻す
                    tmpnum = selectnum + 1;
                    break;
                }    
            }
            selectnum = tmpnum;
        }
    }

    /// <summary>
    /// カーソルを下に移動させる。下の項目がないときはさらに下に移動
    /// </summary>
    /// <param name="nowlist"></param>
    void DownList(int nowlist){
        int tmpnum = nowlist;

        if(nowlist == 0){
            if(selectnum != endlist1.Count-1){
                selectnum++;
            }
            while(!endlist1[selectnum].activeInHierarchy){  //一つ下が空欄だったら繰り返し
                //Debug.Log(endlist1[selectnum].activeInHierarchy);
                selectnum++;
            }
        }else if(nowlist == 1){
            if(selectnum != endlist2.Count-1){
                selectnum++;
            }
            while(!endlist2[selectnum].activeInHierarchy){  //一つ下が空欄だったら繰り返し
                //Debug.Log(endlist2[selectnum].activeInHierarchy);
                selectnum++;
            }
        }
    }

    /// <summary>
    /// 選択しているメニューを強調させるための信号を送る
    /// </summary>
    /// <param name="nowlist"></param>
    void SelectMenu(int nowlist, int selectnum){
        //一旦全部false
        for(int i=0;i<ELS1.Count;i++){
            ELS1[i].setImage(false);
        }
        for(int i=0;i<ELS2.Count;i++){
            ELS2[i].setImage(false);
        }
        //現在選択されてるものをtrue
        if(nowlist == 0){
            ELS1[selectnum].setImage(true);
        }else if(nowlist == 1){
            ELS2[selectnum].setImage(true);
        }
    }

    void EnterMenu(int nowlist, int selectnum){
        if(nowlist == 0){
            ELS1[selectnum].Enter = ELS1[selectnum].Enter ? false : true;
        }else if(nowlist == 1){
            ELS2[selectnum].Enter = ELS2[selectnum].Enter ? false : true;
        }
    }

    void ChangeList(int nowlist){
        for(int i=0;i<biglist.Count;i++){
            if(i == nowlist){
                biglist[i].SetActive(true);
                continue;
            }
            biglist[i].SetActive(false);
        }
        _pageText.SetText((nowlist + 1) + "ページ目");   //テキストも更新
    }

    void ResetSelectandEnter(){
        selectnum = 3;
        for(int i=0;i<ELS1.Count;i++){
            ELS1[i].setImage(false);
            ELS1[i].Enter = false;
        }
        for(int i=0;i<ELS2.Count;i++){
            ELS2[i].setImage(false);
            ELS2[i].Enter = false;
        }
    }
    
    void ReturnTitle(){
        ResetSelectandEnter();
        nowlist = 0;
        TitleCanvas.SetActive(true);
        EndlistCanvas.SetActive(false);
    }
}
