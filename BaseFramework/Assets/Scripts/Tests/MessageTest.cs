using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MessageTest : MonoBehaviour {

    // Use this for initialization
    void Start() {
        MessageMgr.GetInstance().AddEventListener(MessageType.NEXT_BATTALE_START, BattleStart);
        for (int i = 0; i < 1000; i++)
        {
            if (i >= 100)
            {
                MessageMgr.GetInstance().DispatchEvent(new MessageBase(MessageType.NEXT_BATTALE_START, this));
                return;
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }

    void BattleStart(MessageBase cbe)
    {
        Debug.Log("GAME START");
    }


}
