using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCon_TurnDisp : MonoBehaviour
{
    [SerializeField] private EndlistSelect endlistSelect = null;
    [Header("ON/OFFそれぞれ表示したいオブジェクト")]
    [SerializeField] private GameObject TtoDisp = null;
    [SerializeField] private GameObject FtoDisp = null;


    private void OnEnable() {
        TtoDisp.SetActive(endlistSelect.Enter);
        FtoDisp.SetActive(!endlistSelect.Enter);
    }

    // Update is called once per frame
    void Update()
    {
        TtoDisp.SetActive(endlistSelect.Enter);
        FtoDisp.SetActive(!endlistSelect.Enter);
    }
}
