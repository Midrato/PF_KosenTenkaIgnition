using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISetValue : MonoBehaviour
{
    [SerializeField] private GameObject dayText;
    [SerializeField] private GameObject nowActText;
    [SerializeField] private GameObject maxActText;

    private TextMeshProUGUI _dayText;
    private TextMeshProUGUI _nowActText;
    private TextMeshProUGUI _maxActText;

    void Start()
    {
        _dayText = dayText.GetComponent<TextMeshProUGUI>();
        _nowActText = nowActText.GetComponent<TextMeshProUGUI>();
        _maxActText = maxActText.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _dayText.SetText(GloValues.NowDay.ToString());
        _nowActText.SetText(GloValues.NowAct.ToString());
        _maxActText.SetText(GloValues.MaxAct.ToString());
    }
}
