using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(GeneChoiceCustom))]
#endif
*/
public class BookStore_GeneChoice : GeneChoiceCustom
{
    [SerializeField] private bool isShop = true;    //本屋か

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
            if(GloValues.HaveBook[i] == true){
                base.enableChoice[i] = false;
            }
        }
        if(GloValues.HowGoShop >= 2 && isShop){
            base.enableChoice[3] = true;   //合法！魔導書！の解禁
        }
        if(GloValues.HowGoShop == 17 && isShop){
            base.enableChoice[4] = true;   //封書の解禁
        }

        base.ResetChoice(); //enableList弄り終わったから元々の処理を実行
    }
}
