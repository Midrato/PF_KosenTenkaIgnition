using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelect : MonoBehaviour
{
    [SerializeField] private GameObject TitleManager = null;
    [Header("使用される画像オブジェクト")]
    [SerializeField] private GameObject OnImage = null;
    [SerializeField] private GameObject Cursol = null;
    
    [Space]
    
    [Header("上下左右入力した後の移動先")]
    [SerializeField] private GameObject Up = null;
    [SerializeField] private GameObject Down = null;
    [SerializeField] private GameObject Right = null;
    [SerializeField] private GameObject Left = null;
    
    [Space]
    
    [SerializeField] private int MenuNum = 0;   //メニュー判別番号
    
    public bool MoveActive = true;
    
    private TitleManager _TitleManager = null;
    private MenuSelect UpMS = null;
    private MenuSelect DownMS = null;
    private MenuSelect RightMS = null;
    private MenuSelect LeftMS = null;


    public bool Serect = false;
    public bool Enter = false;

    [Space]
    private SoundManager SoundMan;
    
    void Start()
    {
        _TitleManager = TitleManager.GetComponent<TitleManager>();
        if(Up != null){
            UpMS = Up.GetComponent<MenuSelect>();
        }
        if(Down != null){
            DownMS = Down.GetComponent<MenuSelect>();
        }
        if(Right != null){
            RightMS = Right.GetComponent<MenuSelect>();
        }
        if(Left != null){
            LeftMS = Left.GetComponent<MenuSelect>();
        }

        SoundMan = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }



    void Update()
    {
       if(OnImage != null)OnImage.SetActive(Serect);
       if(Cursol != null)Cursol.SetActive(Serect);
       /*if(Serect){
            Debug.Log(string.Format("Get Axis({0}, {1})",Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
       }*/
       
        if(Serect && MoveActive){
            _TitleManager.MenuNum = this.MenuNum;
            //各方向に選ばれたオブジェクトを選択し、自分の選択を解除する
            if(Input.GetKeyDown(KeyCode.UpArrow) && Up != null){
                if(Up.activeInHierarchy){
                    Invoke("delup", 0.03f);
                    
                    this.Serect = false;
                    SoundMan.PlaySE(1);
                }
            }else if(Input.GetKeyDown(KeyCode.DownArrow) && Down != null){
                if(Down.activeInHierarchy){
                    Invoke("deldo", 0.03f);
                    this.Serect = false;
                    SoundMan.PlaySE(1);
                }
            }else if(Input.GetKeyDown(KeyCode.RightArrow) && Right != null){
                if(Right.activeInHierarchy){
                    Invoke("delri", 0.03f);
                    this.Serect = false;
                    SoundMan.PlaySE(1);
                }
            }else if(Input.GetKeyDown(KeyCode.LeftArrow) && Left != null){
                if(Left.activeInHierarchy){
                    Invoke("delle", 0.03f);
                    this.Serect = false;
                    SoundMan.PlaySE(1);
                }
            }else if(Input.GetKeyDown(KeyCode.Space)){
                //エンターされてる状況を保持(押すたびに入れ替わる)
                Enter = Enter ? false : true;
            }
        }
    }

    private void delup(){
        UpMS.Serect = true;
    }
    private void deldo(){
        DownMS.Serect = true;
    }
    private void delri(){
        RightMS.Serect = true;
    }
    private void delle(){
        LeftMS.Serect = true;
    }

    public void SetMove(bool TorF){
        MoveActive = TorF;
    }
}
