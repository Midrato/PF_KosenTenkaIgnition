using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class choicetest : MonoBehaviour
{   
    [SerializeField] private List<UnityEvent> tesEvent = new List<UnityEvent>();

    [SerializeField]
    [TextArea(1, 3)] 
    private List<string> teststr = new List<string>();

    [SerializeField]private Choices choice;

    public void makechoice(){
        choice.CreateChoice(teststr);
    }

    public void TestReturn(int i){
        Debug.Log("Choice:" + i);
    }
}
