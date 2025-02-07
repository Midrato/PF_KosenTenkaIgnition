using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataClearButton : MonoBehaviour
{
    [SerializeField] private bool SelectClear = false;

    [SerializeField] private GameObject Sel_No;
    [SerializeField] private GameObject Sel_Yes;

    [SerializeField] private UnityEvent ClearNo;
    [SerializeField] private UnityEvent ClearYes;

    private SoundManager SoundMan;
    
    private void Start() {
        SoundMan = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }
    
    private void OnEnable() {
        SelectClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        Sel_No.SetActive(!SelectClear);
        Sel_Yes.SetActive(SelectClear);
        
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            SelectClear = false;
            SoundMan.PlaySE(1);
        }else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            SelectClear = true;
            SoundMan.PlaySE(1);
        }else if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space)){
            if(SelectClear){
                ClearYes.Invoke();
            }else{
                ClearNo.Invoke();
            }
        }
    }
}
