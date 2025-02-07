using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerCon;
    [Space]
    //カメラが移動する限界値
    [SerializeField] private float LeftLimit = 0f;
    [SerializeField] private float RightLimit = 13.3f;

    private Transform CameraTrans;
    private Vector3 cameraPos;

    //左右のコライダーに人が入ったか
    private bool isRight = false;
    private bool isLeft = false;


    void Start()
    {
        
    }


    void Update()
    {
        CameraTrans = this.transform;
        cameraPos = CameraTrans.position;

        if(isRight && cameraPos.x < RightLimit){
            if(!playerCon.isDash){
                cameraPos.x += Time.deltaTime * playerCon.WalkSpeed[0];
            }else{
                cameraPos.x += Time.deltaTime * playerCon.WalkSpeed[1];
            }
        }
        if(isLeft && cameraPos.x > LeftLimit){
            if(!playerCon.isDash){
                cameraPos.x -= Time.deltaTime * playerCon.WalkSpeed[0];
            }else{
                cameraPos.x -= Time.deltaTime * playerCon.WalkSpeed[1];
            }
        }

        CameraTrans.position = cameraPos;
        
    }

    //以下イベント用メソッド

    public void RightIn(){
        isRight = true;
    }
    public void RightOut(){
        isRight = false;
    }
    public void LeftIn(){
        isLeft = true;
    }
    public void LeftOut(){
        isLeft = false;
    }
}
