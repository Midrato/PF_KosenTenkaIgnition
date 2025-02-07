using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Book {
        TEXTBOOK,
        ELECTRIC,
        INTERVIEW,
        MAGIC,
        SEAL
    }

public class GloValues : MonoBehaviour
{
    //グローバルに使う、色々な変数やフラグの管理

    public static int NowDay = 1;

    public static int MaxAct = 3;
    public static int NextMaxAct = 3;
    public static int NowAct = 3;

        //  ステータス・所持品情報  //
    /// <summary>
    /// 知識
    /// </summary>
    public static int StatusKnow = 0;
    /// <summary>
    /// 魅力度
    /// </summary>
    public static int StatusCharm = 0;
    
    //0 教科書 1 はじめての電気学 2 面接のすゝめ 3 合法!人心掌握術 4 封書
    public static bool[] HaveBook = new bool[] {true, false, false, false, false};

    //0 合格祈願のお守り  1 厄除けのお守り
    public static bool[] HaveAmulet = new bool[] {false, false};
    
    
    
        
        //  フラグ  //
    public static int ReadInterviewBook = 1;
    public static int ReadMagicBook = 1;
    public static int HowGoShop = 0;
    public static bool BuyBook = false;
    public static bool WatchDayMess = false;    //毎ターン初めのメッセージを見たかどうか
    public static bool GoSecret = false;

    public static bool SabotageSchool = true;  //二日目、学校さぼりか(二日目学校に行ったらfalseに)
    public static bool SabotageExam = true;    //3ターン目、残り行動1で学校の中でなかった場合(3ターン目(ry))
    
    public static int ThisGameEnd = 0;


    public void ResetPlayData(){    //ゲームで使う値全部リセット
        NowDay = 1;
        MaxAct = 3;
        NextMaxAct = 3;
        NowAct = 3;

        StatusKnow = 0;

        StatusCharm = 0;
    
        HaveBook = new bool[] {true, false, false, false, false};

        HaveAmulet = new bool[] {false, false};

        ReadInterviewBook = 1;
        ReadMagicBook = 1;
        BuyBook = false;
        HowGoShop = 0;
        WatchDayMess = false;
        GoSecret = false;

        SabotageSchool = true;
        SabotageExam = true;
    
        ThisGameEnd = 0;
    }

    public void ClearEndings(){
        PlayerPrefs.SetInt("GetEnd1", 0);
        PlayerPrefs.SetInt("GetEnd2", 0);
        PlayerPrefs.SetInt("GetEnd3", 0);
        PlayerPrefs.SetInt("GetEnd4", 0);
        PlayerPrefs.SetInt("GetEnd5", 0);
        PlayerPrefs.SetInt("GetEndex",0);
    }
}