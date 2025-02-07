using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(GeneChoiceCustom))]
#endif
*/
public class DustBox : GeneChoiceCustom
{

    protected override void Start()
    {
        base.Start();
    }


    protected override void Update()
    {
        base.Update();
        
    }

    public override void ResetChoice(){
        //ゴミ箱。所持していなかったら除外
        for(int i=0;i < GloValues.HaveBook.Length;i++){
            if(GloValues.HaveBook[i] == false){
                base.enableChoice[i] = false;
            }
        }
        for(int i=0;i < GloValues.HaveAmulet.Length;i++){
            if(GloValues.HaveAmulet[i] == false){
                base.enableChoice[i + GloValues.HaveBook.Length] = false;
            }
        }

        base.ResetChoice(); //enableList弄り終わったから元々の処理を実行
    }
}
