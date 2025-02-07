using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HowtoPlayManager : MonoBehaviour
{
    [SerializeField] private GameObject TitleCanvas = null;
    [SerializeField] private GameObject HowtoPlayCanvas = null;

    [Header("遊び方説明画像4つ")]
    [SerializeField] private List<GameObject> tutorials = new List<GameObject> {null, null, null, null};
    [Header("ページを表示するテキスト")]
    [SerializeField] private GameObject PageText = null;
    [Space]
    [SerializeField] private int nowLooking = 0;

    private TextMeshProUGUI _PageText = null;

    private SoundManager SoundMan;

    private void Awake() {
        _PageText = PageText.GetComponent<TextMeshProUGUI>();   
        SoundMan = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void OnEnable()
    {
        nowLooking = 0;
        DisplayUpdate(nowLooking);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z)){
            if(nowLooking == tutorials.Count - 1){
                ReturnTitle();

                SoundMan.PlaySE(5);
            }else{
                nowLooking++;
                DisplayUpdate(nowLooking);

                SoundMan.PlaySE(1);
            }
        }else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if(nowLooking != 0){
                nowLooking--;
                DisplayUpdate(nowLooking);
            }
        }
    }
    
    private void DisplayUpdate(int nowSelect){
        //一旦全部false
        for(int i=0;i<tutorials.Count;i++){
            tutorials[i].SetActive(false);
        }

        //現在選択されてるものをtrue
        tutorials[nowLooking].SetActive(true);
        _PageText.SetText((nowSelect + 1) + " / " + tutorials.Count); //ページ数テキストも更新
    }

    void ReturnTitle(){
        TitleCanvas.SetActive(true);
        HowtoPlayCanvas.SetActive(false);
    }
}
