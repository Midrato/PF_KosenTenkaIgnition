using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneralMethods : MonoBehaviour
{
    [SerializeField] private PlayerController Human; //主人公は動きを止めれた方が都合がいい
    [SerializeField] private Image Fade;    //フェードアウト用

    private SoundManager SoundMan;  //SE入れ用

    private bool IsEndDay = false;
    private void Start() {
        //Application.targetFrameRate = 60;
        SoundMan = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, 1f);
        if(GloValues.WatchDayMess){ //日の始まりイベント見てなければ
            stopHuman(0.6f);
        }
        StartCoroutine(FadeIn(-0.3f)); //エリア入りはフェードインで
        //stopHumanDontFree();

    }

    private void Update() {
        string nowSceneName = SceneManager.GetActiveScene().name;
        if(GloValues.NowAct == 0 && Human.CanAct && !IsEndDay && nowSceneName != "EndDayEv" && nowSceneName != "Ending" && nowSceneName != "StageStart"){
            //行動回数がなくなったら日終わりに遷移もここが役を果たす
            IsEndDay = true;
            ChangeScene("EndDayEv");
        }
    }

    public void minusNowAct(int index){
        GloValues.NowAct -= index;
    }
    public void PlusMaxAct(){
        if(GloValues.NextMaxAct < 9){
            GloValues.NextMaxAct++;
        }
        SoundMan.PlaySE(2);
    }
    public void GoNextDay(){
        Invoke("_goNextDay", 0.5f);
    }
    private void _goNextDay(){
        GloValues.NowDay++;
        //次の日の最大行動回数を代入し、次の日の最大行動回数をリセット。残り行動回数も最大まで回復
        GloValues.MaxAct = GloValues.NextMaxAct;
        GloValues.NextMaxAct = 3;
        GloValues.NowAct = GloValues.MaxAct;
        GloValues.WatchDayMess = false;
    }
    public void plusKnow(int index){
        GloValues.StatusKnow += index;
        SoundMan.PlaySE(2);
    }
    public void plusCharm(int index){
        GloValues.StatusCharm += index;
        SoundMan.PlaySE(2);
    }

    public void plusGoShop(){
        GloValues.HowGoShop++;
    }

    public void GetBook(int booknum){
        GloValues.HaveBook[booknum] = true;
        SoundMan.PlaySE(2);
    }
    public void BuyBook(){
        GloValues.BuyBook = true;
    }
    public void LoseBook(int booknum){
        GloValues.HaveBook[booknum] = false;
    }

    public void GetAmulet(int amuletnum){
        GloValues.HaveAmulet[amuletnum] = true;
        SoundMan.PlaySE(2);
    }
    public void LoseAmulet(int amuletnum){
        GloValues.HaveAmulet[amuletnum] = false;
    }

    public void ReadInterviewBook(){
        if(GloValues.ReadInterviewBook < 2){
            GloValues.ReadInterviewBook++;
        }
    }
    public void ReadMagicBook(){
        if(GloValues.ReadMagicBook < 7){
            GloValues.ReadMagicBook++;
        }
    }

    public void FadeOut_In(float darkTime){
        //stopHuman(0.6f + darkTime);
        StartCoroutine(FadeOut());
        StartCoroutine(FadeIn(darkTime));
    }
    public void NotSabotageSchool(){    //二日目、学校さぼらない
        if(GloValues.NowDay == 2){
            GloValues.SabotageSchool = false;
        }
    }
    public void NotSabotageExam(){    //三日目最終行動、試験さぼらない
        if(GloValues.NowDay == 3 && GloValues.NowAct == 1){
            GloValues.SabotageExam = false;
        }
    }

    private IEnumerator FadeOut(){
        Color tmpColI = Fade.color;
        Color endColI = new Color(tmpColI.r, tmpColI.g, tmpColI.b, 1f);
        float chanTimeI = 0f;
        while(chanTimeI < 0.3f){
            chanTimeI += Time.deltaTime;
            Fade.color = Color.Lerp(tmpColI, endColI,  chanTimeI / 0.3f);
            yield return null;
        }
    }

    private IEnumerator FadeIn(float waitTime){
        yield return new WaitForSeconds(waitTime + 0.3f);
        Color tmpCol = Fade.color;
        Color endCol = new Color(tmpCol.r, tmpCol.g, tmpCol.b, 0f);
        float chanTime = 0f;
        while(chanTime < 0.3f){
            chanTime += Time.deltaTime;
            Fade.color = Color.Lerp(tmpCol, endCol,  chanTime/ 0.3f);
            yield return null;
        }
    }

    public void stopHuman(float stopTime){
        Debug.Log("Stop!");
        Human.CanAct = false;
        Invoke("freeHuman", stopTime);
    }
    
    public void stopHumanDontFree(){
        Human.CanAct = false;
    }
    
    public void freeHuman(){
        Human.CanAct = true;
        Debug.Log("Free!");
    }

    public void ChangeScene(string sceneName){
        StartCoroutine(FadeOut());
        stopHuman(0.7f);
        StartCoroutine(_changeScene(sceneName));
    }
    private IEnumerator _changeScene(string _sceneName){
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(_sceneName);
    }

    public void SetEndingNum(){
        int endNum = 0; //添加(学校いる)

        bool isNoItem = true;
        for(int i = 0;i < 4;i++){
            if(isNoItem){
                isNoItem = !GloValues.HaveBook[i];
            }
        }
        for(int i = 0;i < 2;i++){
            if(isNoItem){
                isNoItem = !GloValues.HaveAmulet[i];
            }
        }

        bool End1_2 = GloValues.SabotageExam;   //添加(学校いない)
        bool End2 = ((GloValues.StatusCharm + GloValues.StatusKnow) >= 6 && !GloValues.SabotageSchool && GloValues.HaveAmulet[1]);  //転禍為福
        bool End3 = (GloValues.StatusCharm >= 25 && !GloValues.SabotageExam);   //天下
        bool End4 = ((GloValues.StatusCharm + GloValues.StatusKnow) <= 3 && GloValues.SabotageExam && GloValues.SabotageSchool && isNoItem);    //点火
        bool End5 = (GloValues.StatusCharm >= 5 && GloValues.StatusKnow >= 5 && !GloValues.SabotageExam && !GloValues.SabotageSchool && GloValues.HaveAmulet[0]);   //転科
        
        if(End5){
            endNum = 5;
        }else if(End4){
            endNum = 4;
        }else if(End3){
            endNum = 3;
        }else if(End2){
            endNum = 2;
        }else if(End1_2){
            endNum = 1;
        }

        GloValues.ThisGameEnd =  endNum;
    }

    public void SaveGetEnding(int EndingNum){
        switch(EndingNum){
            case 0:
                PlayerPrefs.SetInt("GetEnd1", 1);
                break;
            case 1:
                PlayerPrefs.SetInt("GetEnd1", 1);
                break;
            case 2:
                PlayerPrefs.SetInt("GetEnd2", 1);
                break;
            case 3:
                PlayerPrefs.SetInt("GetEnd3", 1);
                break;
            case 4:
                PlayerPrefs.SetInt("GetEnd4", 1);
                break;
            case 5:
                PlayerPrefs.SetInt("GetEnd5", 1);
                break;
            case 6:
                PlayerPrefs.SetInt("GetEndex", 1);
                break;
        }
    }
}
