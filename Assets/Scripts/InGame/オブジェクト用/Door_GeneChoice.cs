using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(GeneChoiceCustom))]
#endif
*/
public class Door_GeneChoice : GeneChoiceCustom
{

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()    //ベースから、渡すリストを一時的に変更されたものに変えただけ
    {
        base.Update();
    }

    public override void ResetChoice(){
        if(GloValues.NowDay == 1){
            int index = base.ChoiceTex.IndexOf("学校へ行く");
            base.enableChoice[index] = false;
        }

        base.ResetChoice();
    }
}