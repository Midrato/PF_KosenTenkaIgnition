using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlistSelect : MonoBehaviour
{
    [Header("使用される画像オブジェクト")]
    [SerializeField] private GameObject OnImage = null;

    //public bool Serect = false;
    public bool Enter = false;
    public bool IsReturnButton = false;
    void Start()
    {
        
    }



    /*void Update()
    {

    }*/
    
    public void setImage(bool IsSelect){
        OnImage.SetActive(IsSelect);
    }
}