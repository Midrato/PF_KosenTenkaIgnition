using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(GeneChoiceCustom))]
#endif
*/
public class Desk_GeneChoice : GeneChoiceCustom
{

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
    }

    public override void ResetChoice(){
        //本屋の販売する本を決めていく
        for(int i=0;i < GloValues.HaveBook.Length;i++){
            if(GloValues.HaveBook[i] == false){
                base.enableChoice[i] = false;
            }
        }

        base.ResetChoice(); //enableList弄り終わったから元々の処理を実行
    }
}
