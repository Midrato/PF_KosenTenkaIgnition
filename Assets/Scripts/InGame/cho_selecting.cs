using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cho_selecting : MonoBehaviour
{
    public bool isChoice = false;

    [SerializeField] private GameObject SelColor;
    [SerializeField] private GameObject ChoText;
    
    private TextMeshProUGUI _choText;

    void Awake(){
        _choText = ChoText.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        SelColor.SetActive(isChoice);
    }

    public void SetString(string str){
        _choText.SetText(str);
    }
}
