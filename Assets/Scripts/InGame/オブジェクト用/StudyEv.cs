using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StudyEv : MonoBehaviour
{
    [SerializeField] private Message MessageWindow;
     
    [SerializeField] private List<UnityEvent> StudyEvent = new List<UnityEvent>();
    [TextArea(1, 3)]
    [SerializeField] private List<string> StudyContent = new List<string>();

    void Start()
    {
        
    }

    public void PlayStudyEv(int Mode){
        int nowPlays = 0;
        if(Mode == 0){  //面接のすゝめ
            nowPlays = GloValues.ReadInterviewBook;
        }else if(Mode == 1){
            nowPlays = GloValues.ReadMagicBook;
        }

        StudyEvent[nowPlays-1].Invoke();
        MessageWindow.SetMessagePanel(StudyContent[nowPlays-1]);
    }
}
