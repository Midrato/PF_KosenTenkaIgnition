using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(GeneChoiceCustom))]
#endif
*/
public class AmuletShop : GeneChoiceCustom
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
        //お守り屋。所持していたら除外
        for(int i=0;i < GloValues.HaveAmulet.Length;i++){
            if(GloValues.HaveAmulet[i] == true){
                base.enableChoice[i] = false;
            }
        }

        base.ResetChoice(); //enableList弄り終わったから元々の処理を実行
    }
}
