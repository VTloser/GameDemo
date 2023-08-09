/*
 * FileName:      EventDemo.cs
 * Author:        摩诘创新
 * Date:          2023/08/06 13:14:52
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using DemoGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    // Start is called before the first frame update
    public void Init()
    {
        //EventCenter.AddListener(GameEvent.Begin, (string _, float i) => { Debug.Log("123" + _ + ":" + i); });
        //EventCenter.Broadcast(GameEvent.Begin);


        EventCenter.AddListener(GameEvent.GameBegin, () => { GameManager.Instance.LevelManager.BeginGame(); });


        EventCenter.AddListener(GameEvent.Stop, () => { GameManager.Instance.LevelManager.Stop(); });



    }
}
