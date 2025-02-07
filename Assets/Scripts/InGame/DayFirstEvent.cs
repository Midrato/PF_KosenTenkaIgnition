using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DayFirstEvent : MonoBehaviour
{
    [SerializeField] private List<UnityEvent> DayFirstEvs = new List<UnityEvent>();

    [SerializeField] private GameObject Player; //x = 1.54f
    [SerializeField] private GameObject Camera; //x = 1.35f

    private SpriteRenderer _player;

    void Start(){
        _player = Player.GetComponent<SpriteRenderer>();
        Invoke("_firstEv", 0.03f);
    }

    void _firstEv(){
        if(!GloValues.WatchDayMess){
            GloValues.WatchDayMess = true;
            Player.transform.position = new Vector3(1.54f, Player.transform.position.y, Player.transform.position.z);
            Camera.transform.position = new Vector3(1.35f, Camera.transform.position.y, Camera.transform.position.z);
            _player.flipX = true;

            DayFirstEvs[GloValues.NowDay - 1].Invoke();
            GloValues.WatchDayMess = true;
        }
    }
}
