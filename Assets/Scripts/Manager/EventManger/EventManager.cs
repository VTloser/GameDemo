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
    private GameManager gameMgr;

    public void Init(GameManager gameManager)
    {
        gameMgr = gameManager;

        EventCenter.AddListener(GameEvent.GameBegin, () => { });

        EventCenter.AddListener(GameEvent.GamePause, () =>
        {
            gameMgr.LevelManager.GameStop();
        });

        EventCenter.AddListener(GameEvent.GameFailde, () =>
        {
            gameMgr.LevelManager.GameEnd();
        });
        
        EventCenter.AddListener(GameEvent.LevelSuccess, () =>
        {
            GameManager.Instance.EnemyManager.EnemyPool.DestObjectAll();
        });
        
    }
}
