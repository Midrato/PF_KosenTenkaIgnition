using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookStObjectManager : ObjectManager
{
    [SerializeField]
    [TextArea(1, 3)]private List<string> CanBuyEvStr = new List<string> {"", ""};

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
            base.EventString = !GloValues.BuyBook ? CanBuyEvStr[0] : CanBuyEvStr[1];
            base.Update();
    }
}
