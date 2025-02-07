using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public bool CanAct = true;

    //座標取得用
    private Transform myTrans;
    private Vector3 mypos;

    [SerializeField] private float defaultY = -3.5f;
    private float timeX = 0f;

    [Space]
    [Header("要素0:通常時 要素1:ダッシュ時")]
    public float[] WalkSpeed = new float[] {4f, 7f};
    public float RightLimit = 21.2f;
    public float LeftLimit = -8.2f;

    [SerializeField] private float[] WaveSpeed = new float[] {7.5f, 10f};
    [SerializeField] private float[] WaveHeight = new float[] {0.3f, 0.4f};
    
    [Space]
    public bool isDash = false;
    private int selDash = 0;
    private bool PushDouble = false;

    //スプライト左右反転用
    private SpriteRenderer PlayerRenderer;
    void Start()
    {
        PlayerRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        //CanAct = GloValues.WatchDayMess;
    }

    
    void Update()
    {
        //CanAct = false;
        
        //座標取得
        myTrans = this.transform;
        mypos = myTrans.position;
        
        if(CanAct){
            if(Input.GetKey(KeyCode.LeftShift)){    //Shift押している間ダッシュ有効
                isDash = true;
            }else{
                isDash = false;
            }
            
            selDash = Convert.ToInt32(isDash);

            PushDouble = (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow));

            
            if(Input.GetKey(KeyCode.RightArrow) && mypos.x < RightLimit && !PushDouble){   //右移動　歩く揺れ+ポジション右に 画像のデフォルトが左向きだから画像反転
                WalkWave(WaveSpeed[selDash], WaveHeight[selDash]);

                mypos.x += Time.deltaTime * WalkSpeed[selDash];
                
                PlayerRenderer.flipX = true;

            }else if(Input.GetKey(KeyCode.LeftArrow) && mypos.x > LeftLimit && !PushDouble){  //左移動　歩く揺れ+ポジション左に
                WalkWave(WaveSpeed[selDash], WaveHeight[selDash]);

                mypos.x -= Time.deltaTime * WalkSpeed[selDash];

                PlayerRenderer.flipX = false;

            }else if(mypos.y > defaultY + 0.03f){   //両足付くまで揺らす
                WalkWave(WaveSpeed[selDash], WaveHeight[selDash]);
            }
        }

        if(timeX >= 2 * Mathf.PI){
            timeX = 0;
        }
        //座標確定
        myTrans.position = mypos;
    }

    private void WalkWave(float _WaveSpeed, float _WaveHeight){
        timeX += Time.deltaTime * _WaveSpeed;
        mypos.y = _WaveHeight * (-1f * Mathf.Pow(Mathf.Cos(timeX), 6) + 1f) + defaultY;
    }
}
